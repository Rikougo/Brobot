using UnityEditor;
using Utils;

namespace Editor
{
    [CustomPropertyDrawer(typeof(EnvironmentState))]
    public class EnvironmentStatePropertyDrawer : SerializableDictionaryPropertyDrawer {}
}
