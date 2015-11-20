namespace ReactiveUI.Xaml.Controls.Core
{
    public interface IMergeObject<in T>
    {
        void MergeWith(T other);
    }
}