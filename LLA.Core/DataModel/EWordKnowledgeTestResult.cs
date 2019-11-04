

namespace LLA.Core
{
    /// <summary> Результаты проверки знаний </summary>
    public enum EWordKnowledgeTestResult
    {
        [UiName("Неудачно")]
        Failed = -1,
        
        [UiName("Тест пройден")]
        Passed = 1
    }
}