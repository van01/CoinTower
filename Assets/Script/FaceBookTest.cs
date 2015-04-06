using UnityEngine;
using System.Collections;

public class FaceBookTest : MonoBehaviour {

	private bool _fbInitialized = false;

	// 콜백에 값이 들어오지 않는지 내부에 미리 넣어둔 디버그라벨에 반응이 없습니다.
	void OnFBCallbackLogin(FBResult result)
	{
		if(result.Error == null)
			print("\nFB.Login(Success) :" + result.Text);
		else
			print ("\nFB.Login(Error) : " + result.Error);
	}
	
	// 에디터에서는 디버그라벨에 반응합니다만(기본값이 있기때문인것같고) 안드로이드에서는 반응안합니다.
	void OnFBCallbackInitComplete()
	{
		print("\nFB.Init : Complete(" + FB.IsLoggedIn + ")");
		this._fbInitialized = true;
	}
	
	// 페이스북 처리중에 유니티를 일시정지시키는 것 같은데 디버그라벨에 반응안합니다.
	void OnFBCallbackInitHideUnity(bool isGameShown)
	{
		if (isGameShown == false)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
		
		print ("FB.Init : OnHideUnity(isGameShown - " + isGameShown + ")");
	}
	
	// 미리 넣어둔 UI 버튼의 클릭 메서드입니다. NGUI로 만들었습니다.
	void OnFBLogin()
	{
		// 이 테스트 함수가 호출되면 현재 배경을 바꿉니다. 로그인 버튼을 누를때마다 잘 됩니다.
		print("EventTest()");
		
		// 로그인.
		FB.Login("email,publish_actions", OnFBCallbackLogin);
	}
	
	// 초기화함수.
	void Start()
	{
		// 페이스북 APP ID 확인. 제가 FacebookSetting(unity)에서 기입한 AppId가 화면에 잘 나옵니다.
		print("\nFB.AppId : " + FB.AppId);
		
		// 초기화.
		FB.Init(OnFBCallbackInitComplete, OnFBCallbackInitHideUnity);
	}
	
	// 주기적 처리.
	void Update()
	{
		// 로그인 되었으면 버튼을 비활성화 시키기 위한 체크루틴이지만.
		// 안드로이드 실행시 로그인 버튼을 눌러도 사라지지 않습니다. 로그인창 자체가 안뜨니 당연한 거겠지요.
		if(this._fbInitialized == true && FB.IsLoggedIn == true)
			print("_LoginButton.SetActive(false)");
	}
}
