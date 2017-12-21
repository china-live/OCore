using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XCore.Common;

namespace XCore.Article
{
    public interface IArticleValidator<TArticle> where TArticle : class
    {
        Task<DtoResult> ValidateAsync(ArticleManager<TArticle> manager, TArticle article);
    }

    public partial class ArticleValidator<TArticle> : IArticleValidator<TArticle> where TArticle : class
    {
        public ArticleValidator(ArticleErrorDescriber errors = null)
        {
            Describer = errors ?? new ArticleErrorDescriber();
        }
        public ArticleErrorDescriber Describer { get; private set; }


        public virtual async Task<DtoResult> ValidateAsync(ArticleManager<TArticle> manager, TArticle article)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }
            var errors = new List<DtoError>();


            await ValidateTitle(manager, article, errors);
            //if (manager.Options.User.RequireUniqueEmail)
            //{
            //    await ValidateEmail(manager, user, errors);
            //}
            return errors.Count > 0 ? DtoResult.Failed(errors.ToArray()) : DtoResult.Success;
        }

 
        private async Task ValidateTitle(ArticleManager<TArticle> manager, TArticle article, ICollection<DtoError> errors)
        {
            //var userName = await manager.GetUserNameAsync(user);
            //if (string.IsNullOrWhiteSpace(userName))
            //{
            //    errors.Add(Describer.InvalidUserName(userName));
            //}
            await Task.CompletedTask;
            //var userName = await manager.DeleteAsync(article);
            //    if (string.IsNullOrWhiteSpace(userName))
            //    {
            //        errors.Add(Describer.InvalidUserName(userName));
            //    }
            //    else if (!string.IsNullOrEmpty(manager.Options.User.AllowedUserNameCharacters) &&
            //        userName.Any(c => !manager.Options.User.AllowedUserNameCharacters.Contains(c)))
            //    {
            //        errors.Add(Describer.InvalidUserName(userName));
            //    }
            //    else
            //    {
            //        var owner = await manager.FindByNameAsync(userName);
            //        if (owner != null &&
            //            !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            //        {
            //            errors.Add(Describer.DuplicateUserName(userName));
            //        }
            //    }
            //}
        }
    }


    public interface ITencentVodValidator<TTencentVod> where TTencentVod : class
    {
        Task<DtoResult> ValidateAsync(TencentVodManager<TTencentVod> manager, TTencentVod article);
    }

    public partial class TencentVodValidator<TTencentVod> : ITencentVodValidator<TTencentVod> where TTencentVod : class
    {
        public TencentVodValidator(TencentVodErrorDescriber errors = null)
        {
            Describer = errors ?? new TencentVodErrorDescriber();
        }
        public TencentVodErrorDescriber Describer { get; private set; }


        public virtual async Task<DtoResult> ValidateAsync(TencentVodManager<TTencentVod> manager, TTencentVod article)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }
            var errors = new List<DtoError>();


            await ValidateTitle(manager, article, errors);
            //if (manager.Options.User.RequireUniqueEmail)
            //{
            //    await ValidateEmail(manager, user, errors);
            //}
            return errors.Count > 0 ? DtoResult.Failed(errors.ToArray()) : DtoResult.Success;
        }


        private async Task ValidateTitle(TencentVodManager<TTencentVod> manager, TTencentVod article, ICollection<DtoError> errors)
        {
            //var userName = await manager.GetUserNameAsync(user);
            //if (string.IsNullOrWhiteSpace(userName))
            //{
            //    errors.Add(Describer.InvalidUserName(userName));
            //}
            await Task.CompletedTask;
            //var userName = await manager.DeleteAsync(article);
            //    if (string.IsNullOrWhiteSpace(userName))
            //    {
            //        errors.Add(Describer.InvalidUserName(userName));
            //    }
            //    else if (!string.IsNullOrEmpty(manager.Options.User.AllowedUserNameCharacters) &&
            //        userName.Any(c => !manager.Options.User.AllowedUserNameCharacters.Contains(c)))
            //    {
            //        errors.Add(Describer.InvalidUserName(userName));
            //    }
            //    else
            //    {
            //        var owner = await manager.FindByNameAsync(userName);
            //        if (owner != null &&
            //            !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            //        {
            //            errors.Add(Describer.DuplicateUserName(userName));
            //        }
            //    }
            //}
        }
    }
}
