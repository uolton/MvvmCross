﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Base;
using MvvmCross.Logging;
using MvvmCross.Tests;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace MvvmCross.UnitTest.Mocks.Dispatchers
{
    public class NavigationMockDispatcher 
        : IMvxMainThreadDispatcher, IMvxViewDispatcher
    {
        public readonly List<MvxViewModelRequest> Requests = new List<MvxViewModelRequest>();
        public readonly List<MvxPresentationHint> Hints = new List<MvxPresentationHint>();

        public virtual bool RequestMainThreadAction(Action action, 
                                                    bool maskExceptions = true)
        {
            action();
            return true;
        }

        public virtual Task<bool> ShowViewModel(MvxViewModelRequest request)
        {
            var debugString = $"ShowViewModel: '{request.ViewModelType.Name}' ";
            if (request.ParameterValues != null)
                debugString += $"with parameters: {string.Join(",", request.ParameterValues.Select(pair => $"{{ {pair.Key}={pair.Value} }}"))}";
            else
                debugString += "without parameters";
            MvxTestLog.Instance.Log(MvxLogLevel.Debug, () => debugString);

            Requests.Add(request);
            return Task.FromResult(true);
        }

        public virtual Task<bool> ChangePresentation(MvxPresentationHint hint)
        {
            Hints.Add(hint);
            return Task.FromResult(true);
        }

    }
}
