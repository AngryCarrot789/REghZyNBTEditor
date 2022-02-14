using REghZyMVVM.IoC;

namespace REghZyNBTEditor {
    public interface ITreeViewSelector : IService {
        /// <summary>
        /// Tries to select the given value in all of the currently expanded trees
        /// </summary>
        /// <param name="value"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        bool TrySetExpand(object value, bool isExpanded, bool force = false);

        bool TrySelect(object value, bool force = false);
    }
}