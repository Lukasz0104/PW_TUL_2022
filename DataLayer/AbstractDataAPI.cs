using System;

namespace DataLayer
{
    public abstract class AbstractDataAPI
    {
        public static AbstractDataAPI createDataAPI()
        {
            return new DataAPI();
        }

        private class DataAPI : AbstractDataAPI
        {
            public DataAPI()
            {

            }
        }
    }
}
