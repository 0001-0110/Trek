using YamlDotNet.RepresentationModel;
using static Trek.StoryBuilder;

namespace Trek.Story
{
    internal class StoryLoader
    {
        private readonly StoryBuilder _builder;

        public StoryLoader()
        {
            _builder = new StoryBuilder();
        }

        private void AddLocation(YamlMappingNode location)
        {
            _builder.AddLocation(new LocationBuilder(location["id"].ToString(), location["description"].ToString()));
        }

        private void AddChoice(string sourceId, YamlMappingNode choice)
        {
            _builder.AddChoice(new ChoiceBuilder(choice["directive"].ToString(), choice["description"].ToString(), sourceId, choice["target"].ToString()));
        }

        private void LoadChoices(string sourceId, YamlSequenceNode choices)
        {
            foreach (YamlMappingNode choice in choices.Cast<YamlMappingNode>())
            {
                AddChoice(sourceId, choice);
            }
        }

        private void LoadLocation(YamlMappingNode location)
        {
            AddLocation(location);
            // Get all the choices from this location and add them to the builder
            if (location["choices"] is YamlSequenceNode choices)
                LoadChoices(location["id"].ToString(), choices);
        }

        private void LoadDocument(YamlDocument document)
        {
            YamlMappingNode? root = document.RootNode as YamlMappingNode;
            // Get all nodes stored in this file and create them
            foreach (YamlMappingNode location in (root?["locations"] as YamlSequenceNode)?.Cast<YamlMappingNode>() ?? [])
            {
                LoadLocation(location);
            }
        }

        private void LoadFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                YamlStream yaml = new YamlStream();
                yaml.Load(reader);

                // Get all documents since multiple documents can be in the same files
                foreach (YamlDocument document in yaml.Documents)
                {
                    LoadDocument(document);
                }
            }
        }

        public StoryGraph Load(string path)
        {
            // Get all the files in the given directory, since a story can be splitted in multiple files
            foreach (string file in Directory.EnumerateFiles(path, "*.yaml"))
            {
                LoadFile(file);
            }

            return _builder.Build();
        }
    }
}
