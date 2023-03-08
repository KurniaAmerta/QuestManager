using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using UnityEngine.SceneManagement;

namespace Quest.Login
{
    public class LoginManager : MonoBehaviour
    {
        [Header("Register")]
        [SerializeField] InputField emailRegisInp;
        [SerializeField] InputField passwordRegisInp;
        [SerializeField] InputField usernameRegisInp;
        [SerializeField] Button registerBtn;
        [SerializeField] Button showLoginBtn;
        [SerializeField] GameObject registerObj;

        [Header("Login")]
        [SerializeField] InputField passwordLoginInp;
        [SerializeField] InputField usernameLoginInp;
        [SerializeField] Button loginBtn;
        [SerializeField] Button showRegisterBtn;
        [SerializeField] GameObject loginObj;

        bool isLogin = true;

        private void Awake()
        {
            registerBtn.onClick.AddListener(Register);
            loginBtn.onClick.AddListener(Login);

            showLoginBtn.onClick.AddListener(ShowPanel);
            showRegisterBtn.onClick.AddListener(ShowPanel);
        }

        public void ShowPanel() {
            isLogin = !isLogin;
            registerObj.SetActive(!isLogin);
            loginObj.SetActive(isLogin);
        }

        public void Register()
        {
            if (!string.IsNullOrEmpty(emailRegisInp.text) && !string.IsNullOrEmpty(passwordRegisInp.text) && !string.IsNullOrEmpty(usernameRegisInp.text))
            {
                PlayFabClientAPI.RegisterPlayFabUser(
                    new PlayFab.ClientModels.RegisterPlayFabUserRequest()
                    {
                        Email = emailRegisInp.text,
                        Password = passwordRegisInp.text,
                        Username = usernameRegisInp.text
                    },
                    response =>
                    {
                        PlayerData.Ins.ChangeData(new PlayerDataDetail { 
                            playfabId = response.PlayFabId,
                            username = response.Username
                        });
                        SceneManager.LoadScene(ScenePath.MainMenu);
                    },
                    error =>
                    {
                        Toast.Ins.ShowToast(error.ErrorMessage, 1.5f);
                    }
                );
            }
            else
            {
                Toast.Ins.ShowToast("please fill all fields", 1.5f);
            }
        }

        public void Login()
        {
            if (!string.IsNullOrEmpty(usernameLoginInp.text) && !string.IsNullOrEmpty(passwordLoginInp.text))
            {
                PlayFabClientAPI.LoginWithPlayFab(
                    new PlayFab.ClientModels.LoginWithPlayFabRequest()
                    {
                        Username = usernameLoginInp.text,
                        Password = passwordLoginInp.text
                    },
                    response =>
                    {
                        PlayerData.Ins.ChangeData(new PlayerDataDetail
                        {
                            playfabId = response.PlayFabId,
                            username = usernameLoginInp.text
                        });
                        SceneManager.LoadScene(ScenePath.MainMenu);
                    },
                    error =>
                    {
                        Toast.Ins.ShowToast(error.ErrorMessage, 1.5f);
                    }
                );
            }
            else
            {
                Toast.Ins.ShowToast("please fill all fields", 1.5f);
            }
        }
    }
}
