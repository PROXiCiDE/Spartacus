using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using ProjectCeleste.GameFiles.Tools.Ddt;
using Spartacus.Common;
using SpartacusUtils.Bar;
using SpartacusUtils.DDT;

namespace Spartacus.ViewModels.AiCharacter
{
    [Export(typeof(MilestoneRewardsViewModel))]
    public class MilestoneRewardsViewModel : BasicViewModel, IHandle<CharacterMessageQueue>
    {
        private ImageBrush _backgroundImage;
        private Size _backgroundDimensions;
        private DdtImageBrush _ddtimage;

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

        [ImportingConstructor]
        public MilestoneRewardsViewModel()
        {
            BarFileReader barFileReader = new BarFileReader(_configInfo.ArtUiPath);
            var entry = barFileReader.GetEntry(@"UserInterface\CapitalTech\Milestones\MilestoneTechBackground.ddt");
            if (entry != null)
            {
                _ddtimage = new DdtImageBrush(barFileReader, entry);
                BackgroundDimensions = new Size(_ddtimage.ImageSize.Width, _ddtimage.ImageSize.Height);
                BackgroundImage = _ddtimage.Brush;
            }
        }

        public void Handle(CharacterMessageQueue message)
        {
        }
    }
}
