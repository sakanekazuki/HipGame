using Cysharp.Threading.Tasks;

// フェードキャンバスのインターフェイス
public interface IFadeIOCanvas
{
	void Initialize();
	UniTask FadeIn();
	UniTask FadeOut();
}
