-- Insert default account types
INSERT INTO [dbo].[AccountType] ([AccountTypeID], [AccountTypeName])
VALUES (1, 'Creator'), (2, 'User');

-- Insert default languages
INSERT INTO [dbo].[Language] ([LanguageID], [LanguageName])
VALUES (1, 'English'), (2, 'French'), (3, 'Spanish');

-- Insert default recurring intervals
INSERT INTO [dbo].[Recurring] ([RecurringID], [RecurringName])
VALUES (1, 'Weekly'), (2, 'Bi-Weekly'), (3, 'Monthly'), (4, 'Yearly');
