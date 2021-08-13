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
using System.Collections.Generic;
using iText.Events.Sequence;
using iText.Kernel.Actions;
using iText.Kernel.Actions.Ecosystem;
using iText.Kernel.Pdf;
using iText.Test;

namespace iText.Kernel.Actions.Events {
    public class LinkDocumentIdEventTest : ExtendedITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/kernel/actions/";

        [NUnit.Framework.Test]
        public virtual void DoActionLinkModifiedDocumentBySequenceIdTest() {
            using (ProductEventHandlerAccess access = new ProductEventHandlerAccess()) {
                using (PdfDocument document = new PdfDocument(new PdfReader(SOURCE_FOLDER + "hello.pdf"))) {
                    SequenceId sequenceId = new SequenceId();
                    access.AddEvent(sequenceId, WrapEvent(new ITextTestEvent(sequenceId, null, "sequenceId-testing", "test-product-0"
                        )));
                    access.AddEvent(sequenceId, WrapEvent(new ITextTestEvent(sequenceId, null, "sequenceId-testing", "test-product-1"
                        )));
                    access.AddEvent(sequenceId, WrapEvent(new ITextTestEvent(sequenceId, null, "sequenceId-testing", "test-product-2"
                        )));
                    access.AddEvent(document.GetDocumentIdWrapper(), WrapEvent(new ITextTestEvent(document.GetDocumentIdWrapper
                        (), null, "document-testing", "test-product-3")));
                    access.AddEvent(document.GetDocumentIdWrapper(), WrapEvent(new ITextTestEvent(document.GetDocumentIdWrapper
                        (), null, "document-testing", "test-product-4")));
                    int initialSequenceEventsNumber = access.GetEvents(sequenceId).Count;
                    int initialDocumentEventsNumber = access.GetEvents(document.GetDocumentIdWrapper()).Count;
                    new LinkDocumentIdEvent(document, sequenceId).DoAction();
                    NUnit.Framework.Assert.AreEqual(initialSequenceEventsNumber, access.GetEvents(sequenceId).Count);
                    IList<AbstractProductProcessITextEvent> actualDocumentEvents = access.GetEvents(document.GetDocumentIdWrapper
                        ());
                    NUnit.Framework.Assert.AreEqual(initialDocumentEventsNumber + 3, actualDocumentEvents.Count);
                    for (int i = initialDocumentEventsNumber; i < initialDocumentEventsNumber + 3; i++) {
                        AbstractProductProcessITextEvent sequenceEvent = actualDocumentEvents[i];
                        NUnit.Framework.Assert.AreEqual("sequenceId-testing", sequenceEvent.GetEventType());
                        NUnit.Framework.Assert.AreEqual("test-product-" + (i - initialDocumentEventsNumber), sequenceEvent.GetProductName
                            ());
                        NUnit.Framework.Assert.AreEqual(sequenceId, sequenceEvent.GetSequenceId());
                    }
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void DoActionLinkModifiedDocumentByIdentifiableElemTest() {
            using (ProductEventHandlerAccess access = new ProductEventHandlerAccess()) {
                using (PdfDocument document = new PdfDocument(new PdfReader(SOURCE_FOLDER + "hello.pdf"))) {
                    SequenceId sequenceId = new SequenceId();
                    access.AddEvent(sequenceId, WrapEvent(new ITextTestEvent(sequenceId, null, "sequenceId-testing", "test-product-0"
                        )));
                    access.AddEvent(sequenceId, WrapEvent(new ITextTestEvent(sequenceId, null, "sequenceId-testing", "test-product-1"
                        )));
                    access.AddEvent(sequenceId, WrapEvent(new ITextTestEvent(sequenceId, null, "sequenceId-testing", "test-product-2"
                        )));
                    access.AddEvent(document.GetDocumentIdWrapper(), WrapEvent(new ITextTestEvent(document.GetDocumentIdWrapper
                        (), null, "document-testing", "test-product-3")));
                    access.AddEvent(document.GetDocumentIdWrapper(), WrapEvent(new ITextTestEvent(document.GetDocumentIdWrapper
                        (), null, "document-testing", "test-product-4")));
                    int initialSequenceEventsNumber = access.GetEvents(sequenceId).Count;
                    int initialDocumentEventsNumber = access.GetEvents(document.GetDocumentIdWrapper()).Count;
                    LinkDocumentIdEventTest.IdentifiableElement identifiableElement = new LinkDocumentIdEventTest.IdentifiableElement
                        ();
                    SequenceIdManager.SetSequenceId(identifiableElement, sequenceId);
                    new LinkDocumentIdEvent(document, identifiableElement).DoAction();
                    NUnit.Framework.Assert.AreEqual(initialSequenceEventsNumber, access.GetEvents(sequenceId).Count);
                    IList<AbstractProductProcessITextEvent> actualDocumentEvents = access.GetEvents(document.GetDocumentIdWrapper
                        ());
                    NUnit.Framework.Assert.AreEqual(initialDocumentEventsNumber + 3, actualDocumentEvents.Count);
                    for (int i = initialDocumentEventsNumber; i < initialDocumentEventsNumber + 3; i++) {
                        AbstractProductProcessITextEvent sequenceEvent = actualDocumentEvents[i];
                        NUnit.Framework.Assert.AreEqual("sequenceId-testing", sequenceEvent.GetEventType());
                        NUnit.Framework.Assert.AreEqual("test-product-" + (i - initialDocumentEventsNumber), sequenceEvent.GetProductName
                            ());
                        NUnit.Framework.Assert.AreEqual(sequenceId, sequenceEvent.GetSequenceId());
                    }
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void LinkSimilarEventsButDifferentInstanceTest() {
            using (ProductEventHandlerAccess access = new ProductEventHandlerAccess()) {
                using (PdfDocument document = new PdfDocument(new PdfReader(SOURCE_FOLDER + "hello.pdf"))) {
                    SequenceId sequenceId = new SequenceId();
                    access.AddEvent(sequenceId, new ITextTestEvent(sequenceId, null, "sequenceId-testing", "test-product-1"));
                    access.AddEvent(document.GetDocumentIdWrapper(), new ITextTestEvent(sequenceId, null, "sequenceId-testing"
                        , "test-product-1"));
                    new LinkDocumentIdEvent(document, sequenceId).DoAction();
                    // Check that first event will be linked to document but it was the
                    // similar to stored second event, but they have different instance
                    NUnit.Framework.Assert.AreEqual(3, access.GetEvents(document.GetDocumentIdWrapper()).Count);
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void NullValuesAreAcceptableTest() {
            NUnit.Framework.Assert.DoesNotThrow(() => new LinkDocumentIdEvent(null, (SequenceId)null));
            NUnit.Framework.Assert.DoesNotThrow(() => new LinkDocumentIdEvent(null, (AbstractIdentifiableElement)null)
                );
            NUnit.Framework.Assert.DoesNotThrow(() => new LinkDocumentIdEvent(null, new SequenceId()));
            NUnit.Framework.Assert.DoesNotThrow(() => new LinkDocumentIdEvent(null, new LinkDocumentIdEventTest.IdentifiableElement
                ()));
            using (PdfDocument document = new PdfDocument(new PdfReader(SOURCE_FOLDER + "hello.pdf"))) {
                NUnit.Framework.Assert.DoesNotThrow(() => new LinkDocumentIdEvent(document, (SequenceId)null));
                NUnit.Framework.Assert.DoesNotThrow(() => new LinkDocumentIdEvent(document, (AbstractIdentifiableElement)null
                    ));
            }
        }

        private static ConfirmedEventWrapper WrapEvent(AbstractProductProcessITextEvent @event) {
            return new ConfirmedEventWrapper(@event, "AGPL Version", "iText");
        }

        private class IdentifiableElement : AbstractIdentifiableElement {
        }
    }
}
