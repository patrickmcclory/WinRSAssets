//===================================================================================
// Microsoft patterns & practices
// Silk : Web Client Guidance
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System;
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain.Handlers
{
    public class FulfillReminder
    {
        private readonly IReminderRepository _reminderRepository;

        public FulfillReminder(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public virtual void Execute(int reminderId)
        {
            try
            {
                var reminder = _reminderRepository.GetReminder(reminderId);
                reminder.IsFulfilled = true;
                _reminderRepository.Update(reminder);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToFulfillReminder, ex);
            }
        }



    }
}