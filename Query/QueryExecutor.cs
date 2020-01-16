using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Options;

namespace ThermoRawFileParser.Query
{
    public class QueryExecutor
    {
        private QueryParameters queryParameters;
        
        public QueryExecutor(QueryParameters queryParameters)
        {
            this.queryParameters = queryParameters;
        }


        public int run()
        {
            // put code here
            return 0;
        }
        
        public static HashSet<int> ParseScanIds(String text)
        {
            if (text.Length == 0) throw new OptionException("Scan ID string invalid, nothing specified", null);
            foreach (char c in text)
            {
                int ic = (int) c;
                if (!((ic == (int) ',') || (ic == (int) '-') || (ic == (int) ' ') || ('0' <= ic && ic <= '9')))
                {
                    throw new OptionException("Scan ID string contains invalid character", null);
                }
            }

            string[] tokens = text.Split(new char[] {','}, StringSplitOptions.None);

            HashSet<int> container = new HashSet<int>();

            for (int i = 0; i < tokens.Length; ++i)
            {
                if (tokens[i].Length == 0) throw new OptionException("Scan ID string has invalid format", null);
                string[] rangeBoundaries = tokens[i].Split(new char[] {'-'}, StringSplitOptions.None);
                if (rangeBoundaries.Length == 1)
                {
                    int rangeStart = 0;
                    try
                    {
                        rangeStart = Convert.ToInt32(rangeBoundaries[0]);
                    }
                    catch (Exception e)
                    {
                        throw new OptionException("Scan ID string has invalid format", null);
                    }

                    container.Add(rangeStart);
                }
                else if (rangeBoundaries.Length == 2)
                {
                    int rangeStart = 0;
                    int rangeEnd = 0;
                    try
                    {
                        rangeStart = Convert.ToInt32(rangeBoundaries[0]);
                        rangeEnd = Convert.ToInt32(rangeBoundaries[1]);
                    }
                    catch (Exception e)
                    {
                        throw new OptionException("Scan ID string has invalid format", null);
                    }

                    for (int l = rangeStart; l <= rangeEnd; ++l)
                    {
                        container.Add(l);
                    }
                }
                else throw new OptionException("Scan ID string has invalid format", null);
            }

            return container;
        }
    }
}