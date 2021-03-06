﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace ObjectProgressBars
{
	public static class DictionaryExtentions
    {

        public static bool ContainsKeyPattern<T>(this Dictionary<string, T> dict, string search)
        {
			if (search.Contains("_")) {

				foreach (string key in dict.Keys) {
					if (!key.Contains("_")) continue;

					string[] keyParts = key.Split('_');

					if (search.StartsWith(keyParts[0], StringComparison.Ordinal) 
					    && search.EndsWith(keyParts[1], StringComparison.Ordinal)) {
						return true;
					}
				}
				return false;
			}
			return dict.ContainsKey(search);
        }

        public static T GetItemByKeyPattern<T>(this Dictionary<string, T> dict, string search)
        {
			if (search.Contains("_"))
            {
				foreach (string key in dict.Keys)
                {
                    if (!key.Contains("_")) continue;

                    string[] keyParts = key.Split('_');

                    if (search.StartsWith(keyParts[0], StringComparison.Ordinal)
                        && search.EndsWith(keyParts[1], StringComparison.Ordinal))
                    {
						return dict[key];
                    }
                }
				return default(T);
            }
            return dict[search];
        }

    }
}
