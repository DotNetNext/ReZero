using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ReZero.SuperAPI 
{ 
    public class CodeAnalysisControllerLoaderActionDescriptorChangeProvider : IActionDescriptorChangeProvider
    {
        public static CodeAnalysisControllerLoaderActionDescriptorChangeProvider Instance { get; } = new CodeAnalysisControllerLoaderActionDescriptorChangeProvider();
        public IChangeToken GetChangeToken() => new CancellationChangeToken(_tokenSource.Token);

        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public void NotifyChanges()
        {
            var oldToken = _tokenSource;
            _tokenSource = new CancellationTokenSource();
            oldToken.Cancel();
        }
    }
}
