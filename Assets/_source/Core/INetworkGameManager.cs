using Game.Modifiers;

namespace Game
{
    public interface INetworkGameManager
    {
        public delegate void ScoreChangedDelegate(float newValue);
        public delegate void ScoreChangedWithDeltaDelegate(float newValue, float delta);

        public delegate void ModifierLevelChangedDelegate(RewardModifierSo modifier, int lvl, bool isPositive);


        float PlayerScore { get; }
        float OpponentScore { get; }



        // Для клиента нет совершенно никакого смысла в этом событии,
        // так как он сам прекрасно знает, сколько у него очков, так
        // еще и без задержек, так еще и с правильными дельтами, ведь
        // он обрабатыват клики на своей стороне.
        // Если будет введена возможность получения бонусных очков
        // "извне" (по логике сервера), то вместо синхронизации
        // NetworkVariable, клиенту нужно получить от сервера дельту
        // бонуса. 

        //event ScoreChangedDelegate PlayerScoreChanged;

        // Мы не можем получить настоящую дельту из NetworkVariable,
        // мы получаем только разницу между "снимками", то есть,
        // если тикрейт сервера 1 тик в секунду, а оппонент кликает
        // 10 раз в секунду, получая по 2 очка за каждый клик, то
        // мы каждую секунду будем получать эвент с дельтой в 20,
        // в чем нет совершенно никакого смысла.

        event ScoreChangedDelegate OpponentScoreChanged;

        event ModifierLevelChangedDelegate ModifierLevelChanged;


        void ChangePlayerScore(float delta);
    }
}
