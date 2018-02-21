using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace VisualExplorer.Framework.Format {

    public static class FSizeFixer {

        static FSizeFixer() { }

        private enum SizeRegularType {
            inside,
            beyondWidth,
            beyondHeight,
            outside
        }

        public static Size CalcRecommendSize(Size containerSize, Size contentSize, double edgeOffset) {

            var doubleEdgeOffset = edgeOffset * 2;

            SizeRegularType regularType;
            if (contentSize.Width + doubleEdgeOffset > containerSize.Width) {
                if (contentSize.Height + doubleEdgeOffset > containerSize.Height) {
                    regularType = SizeRegularType.outside;
                } else {
                    regularType = SizeRegularType.beyondWidth;
                }
            } else {
                if (contentSize.Height + doubleEdgeOffset > containerSize.Height) {
                    regularType = SizeRegularType.beyondHeight;
                } else {
                    regularType = SizeRegularType.inside;
                }
            }

            var factor = contentSize.Width / contentSize.Height;
            double recommendWidth = 0, recommendHeight = 0;

            switch (regularType) {
                case SizeRegularType.inside:
                    return contentSize;
                case SizeRegularType.beyondWidth: {
                        recommendWidth = containerSize.Width - doubleEdgeOffset;
                        recommendHeight = recommendWidth / factor;
                    }
                    break;
                case SizeRegularType.beyondHeight: {
                        recommendHeight = containerSize.Height - doubleEdgeOffset;
                        recommendWidth = recommendHeight * factor;
                    }
                    break;
                case SizeRegularType.outside: {
                        var widthFactor = contentSize.Width / containerSize.Width;
                        var heightFactor = contentSize.Height / containerSize.Height;
                        if (widthFactor > heightFactor) {
                            recommendWidth = containerSize.Width - doubleEdgeOffset;
                            recommendHeight = recommendWidth / factor;
                        } else {
                            recommendHeight = containerSize.Height - doubleEdgeOffset;
                            recommendWidth = recommendHeight * factor;
                        }
                    }
                    break;
            }

            return new Size(recommendWidth, recommendHeight);
        }

    }
}
