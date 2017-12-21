using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XCore.Identity.Core.Properties;

namespace XCore.Identity
{
    public interface IUserActiveStore<TUser> : IUserStore<TUser> where TUser : class
    {
        Task<bool> IsActiveAsync(TUser user, CancellationToken cancellationToken);
        Task SetActiveAsync(TUser user, bool active, CancellationToken cancellationToken);
    }

    public class UserActiveManager<TUser> where TUser : class
    {
        public UserActiveManager(IUserStore<TUser> store)
        {
            Store = store;
        }

        protected internal IUserStore<TUser> Store { get; set; }
        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        internal IUserActiveStore<TUser> GetActiveStore(bool throwOnFail = true)
        {
            var cast = Store as IUserActiveStore<TUser>;
            if (throwOnFail && cast == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserActiveStore);
            }
            return cast;
        }

        public virtual async Task<bool> IsActiveAsync(TUser user)
        {
            ThrowIfDisposed();
            var store = GetActiveStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await store.IsActiveAsync(user, CancellationToken);
        }

        public virtual async Task<IdentityResult> ActiveAsync(TUser user)
        {
            ThrowIfDisposed();
            var store = GetActiveStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await store.SetActiveAsync(user, true, CancellationToken);
            return await UpdateUserAsync(user);
        }

        public virtual async Task<IdentityResult> CancelActiveAsync(TUser user)
        {
            ThrowIfDisposed();
            var store = GetActiveStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await store.SetActiveAsync(user, false, CancellationToken);
            return await UpdateUserAsync(user);
        }

        private async Task<IdentityResult> UpdateUserAsync(TUser user)
        {
            return await Store.UpdateAsync(user, CancellationToken);
        }

        /// <summary>
        /// Releases all resources used by the user manager.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                Store.Dispose();
                _disposed = true;
            }
        }
    }
}
