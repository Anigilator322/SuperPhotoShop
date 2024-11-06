using SuperPhotoShop.Infrostructure.Tool_Commands;
using System.Collections.Generic;


namespace SuperPhotoShop.Infrostructure
{
    public class Editor
    {
        private List<Tool> _tools;
        private FileManager _fileManager;

        public void Initialize()
        {
            _tools = new List<Tool>();
            _fileManager = new FileManager();

            LoadTools();
        }

        private void LoadTools()
        {
            _tools.Add(new ColorCorrectionTool());
        }
    }
}
