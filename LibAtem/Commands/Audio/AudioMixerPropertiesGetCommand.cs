using LibAtem.Serialization;

namespace LibAtem.Commands.Audio
{
    [CommandName("AMPP", 8), NoCommandId]
    public class AudioMixerPropertiesGetCommand : SerializableCommandBase
    {
        [Serialize(0), Bool]
        public bool AudioFollowVideo { get; set; }
    }
}