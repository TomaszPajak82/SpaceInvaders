

namespace SoftwareCore.Enumerators
{

    public interface IVersionedIndexed<T>
    {
        int GetVersion();

        bool IsIndexValid(int index);

        T GetValue(int index);
    }

}