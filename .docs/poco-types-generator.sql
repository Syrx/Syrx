declare @property_type table (
    [identifier] int
   ,[moniker] varchar(2)
   ,[type] varchar(50)
);

insert into @property_type (
    [identifier]
   ,[moniker]
   ,[type]
)
values (1, null, null)
      ,(2, 'A', 'PRIMITIVE')
      ,(3, 'B', 'COMPLEX')
      ,(4, 'C', 'GRAPH')
      ,(5, 'D', 'RECORD')
      ,(6, 'E', 'COLLECTION-PRIMITIVE')
      ,(7, 'F', 'COLLECTION-COMPLEX')
      ,(8, 'G', 'COLLECTION-GRAPH')
      ,(9, 'H', 'COLLECTION-RECORD')
	  -- MUTABLE
	  ,(10, 'I', 'PRIMITIVE-MUTABLE')
      ,(11, 'J', 'COMPLEX-MUTABLE')
      ,(12, 'K', 'GRAPH-MUTABLE')
      ,(13, 'L', 'RECORD-MUTABLE')
      ,(14, 'M', 'COLLECTION-PRIMITIVE-MUTABLE')
      ,(15, 'N', 'COLLECTION-COMPLEX-MUTABLE')
      ,(16, 'O', 'COLLECTION-GRAPH-MUTABLE')
      ,(17, 'P', 'COLLECTION-RECORD-MUTABLE')
	  -- INIT
	  ,(18, 'Q', 'PRIMITIVE-INIT')
      ,(19, 'R', 'COMPLEX-INIT')
      ,(20, 'S', 'GRAPH-INIT')
      ,(21, 'T', 'RECORD-INIT')
      ,(22, 'U', 'COLLECTION-PRIMITIVE-INIT')
      ,(23, 'V', 'COLLECTION-COMPLEX-INIT')
      ,(24, 'W', 'COLLECTION-GRAPH-INIT')
      ,(25, 'X', 'COLLECTION-RECORD-INIT');

select [a].[type] [a]
      ,[b].[type] [b]
      ,[c].[type] [c]
      ,[d].[type] [d]
	  ,[e].[type] [e]
	  ,[f].[type] [f]
	  ,[g].[type] [g]
	  ,[h].[type] [h]
	  ,[i].[type] [i]
	  ,[j].[type] [j]
	  ,[k].[type] [k]
	  ,[l].[type] [l]
	  ,[m].[type] [m]
	  ,[n].[type] [n]
	  ,[o].[type] [o]
	  ,[p].[type] [p]
      ,concat(
           [a].[moniker]
          ,[b].[moniker]
          ,[c].[moniker]
          ,[d].[moniker]
          ,[e].[moniker]
          ,[f].[moniker]
          ,[g].[moniker]
          ,[h].[moniker]
          ,[i].[moniker]
          ,[j].[moniker]
          ,[k].[moniker]
          ,[l].[moniker]
          ,[m].[moniker]
          ,[n].[moniker]
          ,[o].[moniker]
          ,[p].[moniker]
       ) [name]
      ,([a].[moniker] + [b].[moniker] + [c].[moniker] + [d].[moniker] + [e].[moniker] + [f].[moniker] + [g].[moniker]
        + [h].[moniker] + [i].[moniker] + [j].[moniker] + [k].[moniker] + [l].[moniker] + [m].[moniker] + [n].[moniker]
        + [o].[moniker] + [p].[moniker]
       ) [sum]
from @property_type [a]
    cross join @property_type [b]
    cross join @property_type [c]
    cross join @property_type [d]
    cross join @property_type [e]
    cross join @property_type [f]
    cross join @property_type [g]
    cross join @property_type [h]
    cross join @property_type [i]
    cross join @property_type [j]
    cross join @property_type [k]
    cross join @property_type [l]
    cross join @property_type [m]
    cross join @property_type [n]
    cross join @property_type [o]
    cross join @property_type [p];

