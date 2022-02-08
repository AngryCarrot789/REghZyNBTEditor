using REghZyIOWrapperV2.Streams;

namespace REghZyNBTEditor.NBT {
    public class NBTTagEndViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.End;

        protected override NBTBaseViewModel CopyInternal() {
            return new NBTTagEndViewModel();
        }

        public override void Read(IDataInput input) {

        }

        public override void Write(IDataOutput output) {

        }
    }
}
