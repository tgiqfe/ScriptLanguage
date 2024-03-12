﻿using System.Collections;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.ObjectGraphVisitors;

namespace ScriptLanguage.Plugin.Serial
{
    /// <summary>
    /// nullやemptyをスキップするObjectGraphVisitor
    /// </summary>
    internal class YamlIEnumerableSkipEmptyObjectGraphVisitor : ChainedObjectGraphVisitor
    {
        public YamlIEnumerableSkipEmptyObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor) : base(nextVisitor)
        {
        }

        public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context)
        {
            var retVal = false;

            if (value.Value == null)
            {
                return false;
            }

            if (value.Value is IEnumerable enumerableObject)
            {
                if (enumerableObject.GetEnumerator().MoveNext())
                {
                    retVal = base.EnterMapping(key, value, context);
                }
            }
            else
            {
                retVal = base.EnterMapping(key, value, context);
            }

            return retVal;
        }
    }
}