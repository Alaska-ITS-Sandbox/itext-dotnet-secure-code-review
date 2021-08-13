/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using System.IO;
using iText.Events.Util;
using iText.IO.Util;
using iText.Test;

namespace iText.IO.Image {
    public class GifTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/io/image/GifTest/";

        [NUnit.Framework.Test]
        public virtual void GifImageTest() {
            using (FileStream file = new FileStream(sourceFolder + "WP_20140410_001.gif", FileMode.Open, FileAccess.Read
                )) {
                byte[] fileContent = StreamUtil.InputStreamToArray(file);
                ImageData img = ImageDataFactory.CreateGif(fileContent).GetFrames()[0];
                NUnit.Framework.Assert.IsTrue(img.IsRawImage());
                NUnit.Framework.Assert.AreEqual(ImageType.GIF, img.GetOriginalType());
            }
        }

        [NUnit.Framework.Test]
        public virtual void GifImageFrameOutOfBoundsTest() {
            Exception e = NUnit.Framework.Assert.Catch(typeof(iText.IO.IOException), () => ImageDataFactory.CreateGifFrame
                (UrlUtil.ToURL(sourceFolder + "image-2frames.gif"), 3));
            NUnit.Framework.Assert.AreEqual(MessageFormatUtil.Format(iText.IO.IOException.CannotFind1Frame, 2), e.Message
                );
        }

        [NUnit.Framework.Test]
        public virtual void GifImageSpecificFrameTest() {
            String imageFilePath = sourceFolder + "image-2frames.gif";
            using (FileStream file = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read)) {
                byte[] fileContent = StreamUtil.InputStreamToArray(file);
                ImageData img = ImageDataFactory.CreateGifFrame(fileContent, 2);
                NUnit.Framework.Assert.AreEqual(100, (int)img.GetWidth());
                NUnit.Framework.Assert.AreEqual(100, (int)img.GetHeight());
                ImageData imgFromUrl = ImageDataFactory.CreateGifFrame(UrlUtil.ToURL(imageFilePath), 2);
                NUnit.Framework.Assert.AreEqual(img.GetData(), imgFromUrl.GetData());
            }
        }

        [NUnit.Framework.Test]
        public virtual void GifImageReadingAllFramesTest() {
            String imageFilePath = sourceFolder + "image-2frames.gif";
            using (FileStream file = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read)) {
                byte[] fileContent = StreamUtil.InputStreamToArray(file);
                IList<ImageData> frames = ImageDataFactory.CreateGifFrames(fileContent);
                NUnit.Framework.Assert.AreEqual(2, frames.Count);
                NUnit.Framework.Assert.AreNotEqual(frames[0].GetData(), frames[1].GetData());
                IList<ImageData> framesFromUrl = ImageDataFactory.CreateGifFrames(UrlUtil.ToURL(imageFilePath));
                NUnit.Framework.Assert.AreEqual(frames[0].GetData(), framesFromUrl[0].GetData());
                NUnit.Framework.Assert.AreEqual(frames[1].GetData(), framesFromUrl[1].GetData());
            }
        }
    }
}
