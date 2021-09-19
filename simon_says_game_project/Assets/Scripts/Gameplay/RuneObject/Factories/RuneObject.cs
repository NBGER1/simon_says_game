using Gameplay.Rune;

namespace Gameplay.RuneObject.Factories
{
    public class RuneObject : Rune.Base.Rune
    {
        #region Fields

        private RuneParams _params;

        #endregion

        #region Methods

        public override void Initialize(RuneParams runeParams)
        {
            _params = runeParams;
            _image = _params.Image;
        }

        public override void SelectRune()
        {
            throw new System.NotImplementedException();
        }

        public override void DeselectRune()
        {
            throw new System.NotImplementedException();
        }

        public override void PlayAudio()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}