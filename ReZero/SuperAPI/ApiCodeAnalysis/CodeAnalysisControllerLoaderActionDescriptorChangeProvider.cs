using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ReZero.SuperAPI 
{ 
  /// <summary>
    /// Provides a mechanism to notify the MVC framework that the set of action descriptors has changed.
    /// Used for dynamic controller/action loading scenarios.
    /// </summary>
    public class CodeAnalysisControllerLoaderActionDescriptorChangeProvider : IActionDescriptorChangeProvider
    {
        /// <summary>
        /// Singleton instance of the change provider.
        /// </summary>
        public static CodeAnalysisControllerLoaderActionDescriptorChangeProvider Instance { get; } = new CodeAnalysisControllerLoaderActionDescriptorChangeProvider();

        /// <summary>
        /// Returns a change token that signals when the action descriptors should be refreshed.
        /// </summary>
        /// <returns>IChangeToken that is triggered on changes.</returns>
        public IChangeToken GetChangeToken() => new CancellationChangeToken(_tokenSource.Token);

        /// <summary>
        /// The current cancellation token source used to signal changes.
        /// </summary>
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        /// <summary>
        /// Notifies all listeners that the action descriptors have changed.
        /// Cancels the current token and creates a new one.
        /// </summary>
        public void NotifyChanges()
        {
            var oldToken = _tokenSource;
            _tokenSource = new CancellationTokenSource();
            oldToken.Cancel();
        }
    }
}
