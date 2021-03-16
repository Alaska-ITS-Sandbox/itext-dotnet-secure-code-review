/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using iText.Kernel.Actions.Events;
using iText.Kernel.Actions.Sequence;
using iText.Kernel.Counter.Event;
using iText.Kernel.Pdf;

namespace iText.Kernel.Actions.Ecosystem {
    public class ITextTestEvent : AbstractITextProductEvent {
        private readonly String eventType;

        private readonly String productName;

        public ITextTestEvent(PdfDocument document, IMetaInfo metaInfo, String eventType, String productName)
            : base(document, metaInfo) {
            this.eventType = eventType;
            this.productName = productName;
        }

        public ITextTestEvent(SequenceId sequenceId, IMetaInfo metaInfo, String eventType, String productName)
            : base(sequenceId, metaInfo) {
            this.eventType = eventType;
            this.productName = productName;
        }

        public override String GetEventType() {
            return eventType;
        }

        public override String GetProductName() {
            return productName;
        }
    }
}
