using Cysharp.Threading.Tasks;

// �t�F�[�h�L�����o�X�̃C���^�[�t�F�C�X
public interface IFadeIOCanvas
{
	void Initialize();
	UniTask FadeIn();
	UniTask FadeOut();
}
