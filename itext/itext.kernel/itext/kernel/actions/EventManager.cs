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
using iText.IO.Util;
using iText.Kernel.Actions.Exceptions;

namespace iText.Kernel.Actions {
    /// <summary>Entry point for event handling mechanism.</summary>
    /// <remarks>
    /// Entry point for event handling mechanism. Class is a singleton,
    /// see
    /// <see cref="GetInstance()"/>.
    /// </remarks>
    public sealed class EventManager {
        private static readonly iText.Kernel.Actions.EventManager INSTANCE = new iText.Kernel.Actions.EventManager
            ();

        private readonly ICollection<IBaseEventHandler> handlers = new LinkedHashSet<IBaseEventHandler>();

        private EventManager() {
            handlers.Add(ProductEventHandler.INSTANCE);
        }

        /// <summary>Allows an access to the instance of EventManager.</summary>
        /// <returns>the instance of the class</returns>
        public static iText.Kernel.Actions.EventManager GetInstance() {
            return INSTANCE;
        }

        /// <summary>Handles the event.</summary>
        /// <param name="event">to handle</param>
        public void OnEvent(IBaseEvent @event) {
            IList<Exception> caughtExceptions = new List<Exception>();
            foreach (IBaseEventHandler handler in handlers) {
                try {
                    handler.OnEvent(@event);
                }
                catch (Exception ex) {
                    caughtExceptions.Add(ex);
                }
            }
            if (@event is AbstractITextConfigurationEvent) {
                try {
                    AbstractITextConfigurationEvent itce = (AbstractITextConfigurationEvent)@event;
                    itce.DoAction();
                }
                catch (Exception ex) {
                    caughtExceptions.Add(ex);
                }
            }
            if (caughtExceptions.Count == 1) {
                throw caughtExceptions[0];
            }
            else {
                if (!caughtExceptions.IsEmpty()) {
                    throw new AggregatedException(AggregatedException.ERROR_DURING_EVENT_PROCESSING, caughtExceptions);
                }
            }
        }

        /// <summary>
        /// Add new
        /// <see cref="IBaseEventHandler"/>
        /// to the event handling process.
        /// </summary>
        /// <param name="handler">is a handler to add</param>
        public void Register(IBaseEventHandler handler) {
            if (handler != null) {
                handlers.Add(handler);
            }
        }

        /// <summary>Check if the handler was registered for event handling process.</summary>
        /// <param name="handler">is a handler to check</param>
        /// <returns>true if handler has been already registered and false otherwise</returns>
        public bool IsRegistered(IBaseEventHandler handler) {
            if (handler != null) {
                return handlers.Contains(handler);
            }
            return false;
        }

        /// <summary>Removes handler from event handling process.</summary>
        /// <param name="handler">is a handle to remove</param>
        /// <returns>
        /// true if the handler had been registered previously and was removed. False if the
        /// handler was not found among registered handlers
        /// </returns>
        public bool Unregister(IBaseEventHandler handler) {
            if (handler != null) {
                return handlers.Remove(handler);
            }
            return false;
        }
    }
}
