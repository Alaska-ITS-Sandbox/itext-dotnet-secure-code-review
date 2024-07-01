/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
Authors: Apryse Software.

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
namespace iText.Layout.Properties.Grid {
    /// <summary>Represents minmax function template value.</summary>
    public class MinMaxValue : FunctionValue {
        private readonly BreadthValue min;

        private readonly BreadthValue max;

        /// <summary>Create a minmax function with a given values.</summary>
        /// <param name="min">min value of a track</param>
        /// <param name="max">max value of a track</param>
        public MinMaxValue(BreadthValue min, BreadthValue max)
            : base(TemplateValue.ValueType.MINMAX) {
            this.min = min;
            this.max = max;
        }

        /// <summary>Gets min template value</summary>
        /// <returns>
        /// 
        /// <see cref="BreadthValue"/>
        /// instance
        /// </returns>
        public virtual BreadthValue GetMin() {
            return min;
        }

        /// <summary>Gets max template value</summary>
        /// <returns>
        /// 
        /// <see cref="BreadthValue"/>
        /// instance
        /// </returns>
        public virtual BreadthValue GetMax() {
            return max;
        }
    }
}