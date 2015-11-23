namespace ReactiveUI.Extensions
{
    public interface IMergeObject<in T>
    {
        void MergeWith(T other);
    }
}