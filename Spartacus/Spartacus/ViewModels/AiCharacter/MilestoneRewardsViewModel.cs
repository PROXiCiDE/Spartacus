using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Spartacus.Common;
using Spartacus.Common.MessageQueue;
using SpartacusUtils.Bar;
using SpartacusUtils.DDT;

namespace Spartacus.ViewModels.AiCharacter
{
    [Export(typeof(MilestoneRewardsViewModel))]
    public class MilestoneRewardsViewModel : BasicViewModel, IHandle<CharacterMessageQueue>
    {
        private Size _backgroundDimensions;
        private ImageBrush _backgroundImage;
        private readonly DdtImageBrush _ddtimage;

        [ImportingConstructor]
        public MilestoneRewardsViewModel()
        {
            var dataBar = _configInfo.BarFileReaders[BarFileEnum.ArtUI];
            var entry = dataBar.GetEntry(@"UserInterface\CapitalTech\Milestones\MilestoneTechBackground.ddt");
            if (entry != null)
            {
                _ddtimage = new DdtImageBrush(dataBar, entry);
                BackgroundDimensions = new Size(_ddtimage.ImageSize.Width, _ddtimage.ImageSize.Height);
                BackgroundImage = _ddtimage.Brush;
            }
        }

        public ImageBrush BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                _backgroundImage = value;
                NotifyOfPropertyChange(nameof(BackgroundImage));
            }
        }

        public Size BackgroundDimensions
        {
            get => _backgroundDimensions;
            set
            {
                _backgroundDimensions = value;
                NotifyOfPropertyChange(nameof(BackgroundDimensions));
            }
        }

        public void Handle(CharacterMessageQueue message)
        {
        }
    }
}