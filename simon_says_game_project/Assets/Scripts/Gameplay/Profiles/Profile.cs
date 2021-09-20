using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Avatars
{
    public class Profile : MonoBehaviour
    {
        #region Editor

        [SerializeField] private RawImage _image;
        [SerializeField] private TextMeshProUGUI _name;

        #endregion

        #region Properties

        public void SetImageTexture(Texture2D _texture)
        {
            _image.texture = _texture;
        }

        public void SetName(string name)
        {
            _name.text = name;
        }

        #endregion
    }
}