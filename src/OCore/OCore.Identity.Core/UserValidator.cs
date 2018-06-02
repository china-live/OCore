using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCore.Identity.Extensions;

namespace OCore.Identity
{
    public class AppUserValidator<TUser> : UserValidator<TUser> where TUser : User
    {
        public AppUserValidator(
            IOptions<OCoreUserOptions> optionsAccessor,
            AppIdentityErrorDescriber errors = null) :base(errors) {

            Describer2 = errors ?? new AppIdentityErrorDescriber();
            Options = optionsAccessor?.Value ?? new OCoreUserOptions();
        }
        public OCoreUserOptions Options { get; set; }
        public AppIdentityErrorDescriber Describer2 { get; private set; }

        /// <summary>
        /// Validates the specified <paramref name="user"/> as an asynchronous operation.
        /// </summary>
        /// <param name="manager">The <see cref="UserManager{TUser}"/> that can be used to retrieve user properties.</param>
        /// <param name="user">The user to validate.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the validation operation.</returns>
        public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var errors = new List<IdentityError>();
            await ValidateUserName(manager, user, errors);
            if (manager.Options.User.RequireUniqueEmail)
            {
                await ValidateEmail(manager, user, errors);
            }

            //注意：手机、姓名和Email不同之处，Email是系统注册必须的，手机和姓名是否必须可以配置文件配置
            if (Options.RequireUniquePhoneNumber)
            {
                await ValidatePhoneNumber(manager, user, errors);
            }
            if (Options.RequireUniqueFullName)
            {
                await ValidateFullName(manager, user, errors);
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        private async Task ValidateUserName(UserManager<TUser> manager, TUser user, ICollection<IdentityError> errors)
        {
            var userName = await manager.GetUserNameAsync(user);
            if (string.IsNullOrWhiteSpace(userName))
            {
                errors.Add(Describer.InvalidUserName(userName));
            }
            else if (!string.IsNullOrEmpty(manager.Options.User.AllowedUserNameCharacters) && 
                userName.Any(c => !manager.Options.User.AllowedUserNameCharacters.Contains(c)))
            {
                errors.Add(Describer.InvalidUserName(userName));
            }
            else
            {
                var owner = await manager.FindByNameAsync(userName);
                if (owner != null &&
                    !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
                {
                    errors.Add(Describer.DuplicateUserName(userName));
                }
            }
        }
 
        private async Task ValidateEmail(UserManager<TUser> manager, TUser user, List<IdentityError> errors)
        {
            var email = await manager.GetEmailAsync(user);
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(Describer.InvalidEmail(email));
                return;
            }
            if (!new EmailAddressAttribute().IsValid(email))
            {
                errors.Add(Describer.InvalidEmail(email));
                return;
            }
            var owner = await manager.FindByEmailAsync(email);
            if (owner != null && !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            {
                errors.Add(Describer.DuplicateEmail(email));
            }
        }

        private async Task ValidatePhoneNumber(UserManager<TUser> manager, TUser user, List<IdentityError> errors)
        {
            var phone = await manager.GetPhoneNumberAsync(user);
            if (Options.RequirePhoneNumber && string.IsNullOrWhiteSpace(phone))
            {
                errors.Add(Describer2.InvalidPhoneNumber(phone));
                return;
            }
            var owner = await manager.FindByPhoneNumberAsync(phone);
            if (owner != null && !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            {
                errors.Add(Describer2.DuplicatePhoneNumber(phone));
            }
        }

        private async Task ValidateFullName(UserManager<TUser> manager, TUser user, List<IdentityError> errors)
        {
            var fullName = await manager.GetFullNameAsync(user);
            if (Options.RequireFullName && string.IsNullOrWhiteSpace(fullName))
            {
                errors.Add(Describer2.InvalidFullName(fullName));
                return;
            }
            var owner = await manager.FindByFullNameAsync(fullName);
            if (owner != null && !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            {
                errors.Add(Describer2.DuplicateFullName(fullName));
            }
        }
    }

    public class AppIdentityErrorDescriber:IdentityErrorDescriber {
        public virtual IdentityError InvalidFullName(string fullName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidFullName),
                Description = "请提供您的真实姓名"
            };
        }

        public virtual IdentityError DuplicateFullName(string fullName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateFullName),
                Description = "用户姓名被占用"
            };
        }

        public virtual IdentityError InvalidPhoneNumber(string phoneNumber)
        {
            return new IdentityError
            {
                Code = nameof(InvalidPhoneNumber),
                Description = "手机号码不能为空"
            };
        }

        public virtual IdentityError DuplicatePhoneNumber(string phoneNumber)
        {
            return new IdentityError
            {
                Code = nameof(DuplicatePhoneNumber),
                Description = "手机号码已被占用"
            };
        }
    }
}
