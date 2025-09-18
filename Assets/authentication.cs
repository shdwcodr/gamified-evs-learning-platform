using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

public class Auth : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Text feedbackText;

    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void Login()
    {
        string email = usernameInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                feedbackText.text = "Login failed: " + task.Exception?.Message;
            }
            else
            {
                FirebaseUser user = task.Result;
                feedbackText.text = "Login successful! Welcome " + user.Email;
                SceneManager.LoadScene("GameScene"); // replace with your actual scene
            }
        });
    }

    public void Register()
    {
        string email = usernameInput.text;
        string password = passwordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                feedbackText.text = "Registration failed: " + task.Exception?.Message;
            }
            else
            {
                FirebaseUser newUser = task.Result;
                feedbackText.text = "Account created! You can now login.";
            }
        });
    }
}

void Start()
{
    auth = FirebaseAuth.DefaultInstance;
    if (auth.CurrentUser != null)
    {
        // User already logged in, go to game directly
        SceneManager.LoadScene("GameScene");
    }
}

