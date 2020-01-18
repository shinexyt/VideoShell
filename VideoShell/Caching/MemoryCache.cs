// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Threading;
using System.Diagnostics;

namespace VideoShell.Caching
{
    public class MemoryCache
    {
        Dictionary<string, object> memoryDictionary;
        private static readonly object s_initLock = new object();
        private static MemoryCache s_defaultCache;
        public static MemoryCache Default
        {
            get
            {
                if (s_defaultCache == null)
                {
                    lock (s_initLock)
                    {
                        if (s_defaultCache == null)
                        {
                            s_defaultCache = new MemoryCache();
                        }
                    }
                }
                return s_defaultCache;
            }
        }
        private MemoryCache()
        {
            memoryDictionary = new Dictionary<string, object>();
        }
        public object this[string key]
        {
            get
            {
                memoryDictionary.TryGetValue(key, out object result);
                return result;
            }
            set
            {
                memoryDictionary[key] = value;
            }
        }

    }
}
