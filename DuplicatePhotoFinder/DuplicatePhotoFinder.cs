using System.Security.Cryptography;
using System.Windows.Forms;

namespace DuplicatePhotoFinder
{
    public partial class DuplicatePhotoFinder : Form
    {
        private string selectedFolderPath = string.Empty;
        private List<List<string>> duplicateGroups = new List<List<string>>();

        public DuplicatePhotoFinder()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a folder to scan for duplicate images.";
                folderDialog.ShowNewFolderButton = false;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolderPath = folderDialog.SelectedPath;
                    folderPathLabel.Text = selectedFolderPath; // Display the folder path
                }
            }
        }

        private void ScanForDuplicates()
        {
            if (string.IsNullOrEmpty(selectedFolderPath))
            {
                MessageBox.Show("Please select a folder first.");
                return;
            }

            // Get all image files (modify filters to include the image formats you need)
            var imageFiles = Directory.GetFiles(selectedFolderPath, "*.*", SearchOption.AllDirectories).Where(f => f.EndsWith(".jpg") || f.EndsWith(".png") || f.EndsWith(".jpeg")).ToList();

            Dictionary<string, List<string>> fileHashGroups = new Dictionary<string, List<string>>();

            // Calculate hash for each image
            foreach (var file in imageFiles)
            {
                string fileHash = GetFileHash(file);
                if (!fileHashGroups.ContainsKey(fileHash))
                {
                    fileHashGroups[fileHash] = new List<string>();
                }
                fileHashGroups[fileHash].Add(file);
            }

            // Identify duplicates (groups with more than one file)
            duplicateGroups = fileHashGroups.Values.Where(g => g.Count > 1).ToList();

            // Prompt user for each group of duplicates
            foreach (var duplicateGroup in duplicateGroups)
            {
                ShowDuplicatePrompt(duplicateGroup);
            }
        }

        private string GetFileHash(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }

        private void ShowDuplicatePrompt(List<string> duplicateGroup)
        {
            Form prompt = new Form();
            prompt.Text = "Duplicates Found";
            prompt.StartPosition = FormStartPosition.CenterScreen;

            Label instructions = new Label() { Text = "Select the image(s) you want to keep. The rest will be deleted.", AutoSize = true, Left = 10, Top = 10 };

            FlowLayoutPanel imagePanel = new FlowLayoutPanel() { Left = 10, Top = 40, Width = 600, Height = 300, AutoScroll = true };  // Container for image thumbnails and checkboxes

            List<CheckBox> checkBoxes = new List<CheckBox>();

            foreach(var file in duplicateGroup)
            {
                // Load image thumbnail
                PictureBox thumbnail = new PictureBox();

                //thumbnail.Image = Image.FromFile(file);
                Image image;
                using (var tempImage = Image.FromFile(file))
                {
                    image = new Bitmap(tempImage);
                }
                thumbnail.Image = image;

                thumbnail.SizeMode = PictureBoxSizeMode.Zoom; // Ensure image fits within the size
                thumbnail.Width = 100;
                thumbnail.Height = 100;

                // Checkbox for the file
                CheckBox cb = new CheckBox() { Text = file, AutoSize = true, Width = 500 };
                checkBoxes.Add(cb);

                //FlowLayoutPanel container = new FlowLayoutPanel() { Width = 500, Height = 120 };  // Group each thumbnail and checkbox together

                FlowLayoutPanel container = new FlowLayoutPanel()
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    MaximumSize = new Size(580, 120),
                    Margin = new Padding(5)
                };

                container.Controls.Add(thumbnail);
                container.Controls.Add(cb);

                imagePanel.Controls.Add(container);
            }

            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 350 };

            confirmation.Click += (sender, e) =>
            {
                // Get all selected files
                var selectedFiles = checkBoxes.Where(cb => cb.Checked).Select(cb => cb.Text).ToList();

                if (selectedFiles.Count == 0)
                {
                    MessageBox.Show("Please select at least one image to keep.");
                    return;
                }

                // Delete all files not selected
                foreach (var file in duplicateGroup)
                {
                    if (!selectedFiles.Contains(file))
                    {
                        File.Delete(file);
                    }
                }
                prompt.Close();
            };

            prompt.Controls.Add(instructions);
            prompt.Controls.Add(imagePanel);
            prompt.Controls.Add(confirmation);
            prompt.Width = 600;
            prompt.Height = 450;
            prompt.ShowDialog();
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            ScanForDuplicates();
        }
    }
}
