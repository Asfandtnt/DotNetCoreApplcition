using IBM.Data.DB2.Core;
//using IBM.Data.Db2;

namespace DotNetCoreApplcition
{
    public interface IDataObject
    {
        void FillData(DB2DataReader reader);
    }
}
