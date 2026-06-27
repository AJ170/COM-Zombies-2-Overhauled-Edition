using UnityEngine;
using UnityEngine.PSVita;

public class ExampleRenderTexturePlayback : MonoBehaviour
{
    public string m_MoviePath;
    public RenderTexture m_RenderTexture;
    public GUISkin m_Skin;
	bool m_IsPlaying = false;

    void Start()
    {
        PSVitaVideoPlayer.Init(m_RenderTexture);
        PSVitaVideoPlayer.Play(m_MoviePath, PSVitaVideoPlayer.Looping.Continuous, PSVitaVideoPlayer.Mode.RenderToTexture);
    }

    void OnPreRender()
    {
        PSVitaVideoPlayer.Update();
    }

    void OnGUI()
    {
        GUI.skin = m_Skin;
        GUILayout.BeginArea(new Rect(10,10,50,Screen.height));
        if (GUILayout.Button("Skip"))
        {
			if (m_IsPlaying)
			{
				PSVitaVideoPlayer.Stop();
                GameInitUIController init = FindObjectOfType<GameInitUIController>();
                if (init != null)
                    init.BypassPrivateScene();
                else
                    Debug.LogWarning("GameInitUIController not in the active scene — can't bypass.");
            }
			else
			{
                GameInitUIController init = FindObjectOfType<GameInitUIController>();
                if (init != null)
                    init.BypassPrivateScene();
                else
                    Debug.LogWarning("GameInitUIController not in the active scene — can't bypass.");
            }
        }
        GUILayout.EndArea();
    }

	void OnMovieEvent(int eventID)
	{
		PSVitaVideoPlayer.MovieEvent movieEvent = (PSVitaVideoPlayer.MovieEvent)eventID;
		switch (movieEvent)
		{
			case PSVitaVideoPlayer.MovieEvent.PLAY:
				m_IsPlaying = true;
				break;

			case PSVitaVideoPlayer.MovieEvent.STOP:
				m_IsPlaying = false;
                GameInitUIController init = FindObjectOfType<GameInitUIController>();
                if (init != null)
                    init.BypassPrivateScene();
                else
                    Debug.LogWarning("GameInitUIController not in the active scene — can't bypass.");
                break;
		}
	}
}
