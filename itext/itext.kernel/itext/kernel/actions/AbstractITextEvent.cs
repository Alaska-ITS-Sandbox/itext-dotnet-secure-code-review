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

namespace iText.Kernel.Actions {
    /// <summary>Abstract class which defines events only for internal usage.</summary>
    public abstract class AbstractITextEvent : IBaseEvent {
        private const String INTERNAL_PACKAGE = "iText";

        private const String ONLY_FOR_INTERNAL_USE = "AbstractITextEvent is only for internal usage.";

        /// <summary>Creates an instance of abstract iText event.</summary>
        /// <remarks>Creates an instance of abstract iText event. Only for internal usage.</remarks>
        public AbstractITextEvent() {
            if (!this.GetType().FullName.StartsWith(INTERNAL_PACKAGE)) {
                throw new NotSupportedException(ONLY_FOR_INTERNAL_USE);
            }
        }
    }
}