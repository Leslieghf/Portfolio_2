using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Portfolio_2.UI
{
    public abstract class Panel : MonoBehaviour
    {
        public void SetVisibility(bool visibility)
        {
            gameObject.SetActive(visibility);
        }

        public bool GetVisibility()
        {
            return gameObject.activeSelf;
        }

        public void ToggleVisibility()
        {
            if (GetVisibility())
            {
                SetVisibility(false);
            }
            else
            {
                SetVisibility(true);
            }
        }
    } 
}
