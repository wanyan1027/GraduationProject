create database AestheticismMS
drop database AestheticismMS
go

use AestheticismMS

--�û���
create table UserMusic
(
	[UserID] [int] primary key IDENTITY(1,1) NOT NULL,  --�û�ID
	[UserName] [nvarchar](20) NOT NULL,  --�˺�
	[UserLoginName] [nvarchar](20)  NOT NULL,  --�û��ǳ�
	[UserLoginPwd] [nvarchar](20) NOT NULL,  --�û�����
	[UserSongSheet] [nvarchar](100),  --�û��赥
	[UserSex] nvarchar(5) check([UserSex]='��' or [UserSex]='Ů') NOT NULL,  --�û��Ա�
	[UserPhone] [nvarchar](20) NULL,   --�û��绰
	[UserBirthday] [nvarchar](20) NULL,  --�û�����
	[UserEmail] [nvarchar](50) NULL,  --�û�����
)
select * from UserMusic
insert into UserMusic values
('Sina','ϯ��','111111','','��','123456789','2017-8-3','405-575-227'),
('Rabbit','С��','123123','','Ů','12345678911','2015-2-5','348-858-223'),
('Monkey','С��','154326','','��','42577689634','2020-2-5','324-220-145'),
('WanYan','¹¹','123456','','Ů','13873099665','2018-10-14','753-357-225'),
('Xiyang','Ϧ��','123456','','��','98745632100','2011-7-25','752-758-553'),
('Baiyang','����','123456','','��','1236547890','2013-9-5','277-247-125'),
('xihuan','����','123456','','��','0123654789','2010-4-5','763-753-865')
--�������

go
--����Ա��
create table Administer
(
	[AdministerID] [int] primary key IDENTITY(1,1) NOT NULL,  --����ԱID
	[AdministerName] [nvarchar](20) NOT NULL,  --����Ա�ǳ�
	[AdministerLoginName] [nvarchar](20) NOT NULL,  --����Ա�˺�
	[AdministerLoginPwd] [nvarchar](20) NOT NULL,  --����Ա����
	[AdministerPhone] [nvarchar](20) NULL,  --����Ա�绰
	[AdministerEmail] [nvarchar](50) NULL  --����Ա����
)
select * from Administer
insert into Administer values
('¹¹','WanYan','123456','13873099665',''),
('Admin','Admin','123456','13773069835','')

--���ֱ�
create table Singer
(
SingerID int primary key identity(1,1) not null, --���ֱ��
SingerName nvarchar(20) not null,  --��������
[SingerSex] nvarchar(5) check([SingerSex]='��' or [SingerSex]='Ů') NOT NULL,--�����Ա�
SingerPohoto nvarchar(100)  --����ͷ��
)
select * from Singer
insert into Singer values
('�ֿ���','��','1.jpg'),
('�ܽ���','��','2.jpg'),
('¹��','��','3.jpg'),
('��ޱ','Ů','4.jpg'),
('˾��','Ů','5.jpg'),
('���ζ�','��','6.jpg')

go
--�����б�
create table MusicList
(
	MusicListID int primary key identity(1,1) not null,  --����ID
	MusicListName nvarchar(20) not null,  --������
	MusicListFileSize nvarchar(200),  --�����ļ���С
	MusicListPath nvarchar(50),  --������ַ
	SingerID [int] foreign key(SingerID) references Singer(SingerID),  --���ֱ��
	[UserID] [int] foreign key([UserID]) references UserMusic([UserID])   --����������,һ�㶼����id��Ϊ����
)
select * from MusicList
insert into MusicList values
('��������','5.0M','...',2,1),
('С����','2.3M','...',2,2),
('�໨��','3.4M','...',3,3),
('ŵ��','5.2M','...',4,4),
('Lucky Lucky','2.0M','...',5,5),
('����','3.5M','...',6,6),
('�ͱ���','4.8M','...',7,7)
--����-�Ǹ������ͣ��������ͺ͸������Ͷ�Ҫ
create table MusicListType
(
MusicListTypeID int primary key identity(1,1) not null,
SingerID [int] foreign key(SingerID) references Singer(SingerID), --����ID
MusicListID [int] foreign key(MusicListID) references MusicList(MusicListID),  --����ID
SingerType nvarchar(10) ,  --���ַ���
MusicListType nvarchar(10) ,  --���ַ���
)
select * from MusicListType
insert into MusicListType values
(2,2,'�и���','����'),
(3,3,'�и���','����'),
(4,4,'�и���','����'),
(5,5,'�и���','��ŵ'),
(6,6,'Ů����','���'),
(7,7,'Ů����','����'),
(2,1,'�и���','����')

go
--���۱�
create table Discuss
(
DiscussID int primary key identity(1,1) not null,  --���
DiscussContent nvarchar(100) not null,  --��������
DiscussTime datetime ,--����ʱ��
[UserID][int] foreign key([UserID]) references UserMusic([UserID]),  --�û����
MusicListID [int] foreign key(MusicListID) references MusicList(MusicListID)  --�������
)
go


insert into Discuss values
('�ú����������޵к���������������','2019-5-3',1,5),
('�ú����������޵к���������������','2015-5-3',2,1),
('�ú����������޵к���������������','2013-5-3',4,5),
('�ú����������޵к���������������','2012-5-3',2,6),
('�ú����������޵к���������������','2016-5-3',3,7),
('�ú����������޵к���������������','2019-5-15',5,8)
select * from Discuss