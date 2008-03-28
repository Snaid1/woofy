﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Woofy.Entities;
using Woofy.DatabaseAccess;

namespace Woofy
{
    public static class ExtensionMethods
    {
        public static T GetValue<T>(this DbDataReader reader, string columnName)
        {
            object value = reader.GetValue(reader.GetOrdinal(columnName));
            if (value == DBNull.Value)
                value = null;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T Read<T>(this DbDataReader reader)
            where T : class
        {
            if (!reader.Read())
                return null;

            Type type = typeof(T);
            
            if (type == typeof(ComicDefinition))
                return (T)EntityModel.ReadComicDefinition(reader);
            else if (type == typeof(Comic))
                return (T)EntityModel.ReadComic(reader);
            else if (type == typeof(ComicStrip))
                return (T)EntityModel.ReadComicStrip(reader);

            return null;            
        }        
    }
}
