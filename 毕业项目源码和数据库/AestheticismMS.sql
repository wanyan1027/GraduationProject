create database AestheticismMS
drop database AestheticismMS
go

use AestheticismMS

--用户表
create table UserMusic
(
	[UserID] [int] primary key IDENTITY(1,1) NOT NULL,  --用户ID
	[UserName] [nvarchar](20) NOT NULL,  --账号
	[UserLoginName] [nvarchar](20)  NOT NULL,  --用户昵称
	[UserLoginPwd] [nvarchar](20) NOT NULL,  --用户密码
	[UserSongSheet] [nvarchar](100),  --用户歌单
	[UserSex] nvarchar(5) check([UserSex]='男' or [UserSex]='女') NOT NULL,  --用户性别
	[UserPhone] [nvarchar](20) NULL,   --用户电话
	[UserBirthday] [nvarchar](20) NULL,  --用户生日
	[UserEmail] [nvarchar](50) NULL,  --用户邮箱
)
select * from UserMusic
insert into UserMusic values
('Sina','席纳','111111','','男','123456789','2017-8-3','405-575-227'),
('Rabbit','小兔','123123','','女','12345678911','2015-2-5','348-858-223'),
('Monkey','小猴','154326','','男','42577689634','2020-2-5','324-220-145'),
('WanYan','鹿鹿','123456','','女','13873099665','2018-10-14','753-357-225'),
('Xiyang','夕阳','123456','','男','98745632100','2011-7-25','752-758-553'),
('Baiyang','白杨','123456','','男','1236547890','2013-9-5','277-247-125'),
('xihuan','西幻','123456','','男','0123654789','2010-4-5','763-753-865')
--添加数据

go
--管理员表
create table Administer
(
	[AdministerID] [int] primary key IDENTITY(1,1) NOT NULL,  --管理员ID
	[AdministerName] [nvarchar](20) NOT NULL,  --管理员昵称
	[AdministerLoginName] [nvarchar](20) NOT NULL,  --管理员账号
	[AdministerLoginPwd] [nvarchar](20) NOT NULL,  --管理员密码
	[AdministerPhone] [nvarchar](20) NULL,  --管理员电话
	[AdministerEmail] [nvarchar](50) NULL  --管理员邮箱
)
select * from Administer
insert into Administer values
('鹿鹿','WanYan','123456','13873099665',''),
('Admin','Admin','123456','13773069835','')

--歌手表
create table Singer
(
SingerID int primary key identity(1,1) not null, --歌手编号
SingerName nvarchar(20) not null,  --歌手名字
[SingerSex] nvarchar(5) check([SingerSex]='男' or [SingerSex]='女') NOT NULL,--歌手性别
SingerPohoto nvarchar(100)  --歌手头像
)
select * from Singer
insert into Singer values
('林俊杰','男','1.jpg'),
('周杰伦','男','2.jpg'),
('鹿晗','男','3.jpg'),
('戚薇','女','4.jpg'),
('司南','女','5.jpg'),
('王嘉尔','男','6.jpg')

go
--音乐列表
create table MusicList
(
	MusicListID int primary key identity(1,1) not null,  --歌曲ID
	MusicListName nvarchar(20) not null,  --歌曲名
	MusicListFileSize nvarchar(200),  --歌曲文件大小
	MusicListPath nvarchar(50),  --歌曲地址
	SingerID [int] foreign key(SingerID) references Singer(SingerID),  --歌手编号
	[UserID] [int] foreign key([UserID]) references UserMusic([UserID])   --歌曲发布者,一般都是那id作为主键
)
select * from MusicList
insert into MusicList values
('修炼爱情','5.0M','...',2,1),
('小酒窝','2.3M','...',2,2),
('青花瓷','3.4M','...',3,3),
('诺言','5.2M','...',4,4),
('Lucky Lucky','2.0M','...',5,5),
('冬眠','3.5M','...',6,6),
('巴比龙','4.8M','...',7,7)
--类型-是歌曲类型？歌曲类型和歌手类型都要
create table MusicListType
(
MusicListTypeID int primary key identity(1,1) not null,
SingerID [int] foreign key(SingerID) references Singer(SingerID), --歌手ID
MusicListID [int] foreign key(MusicListID) references MusicList(MusicListID),  --歌曲ID
SingerType nvarchar(10) ,  --歌手分类
MusicListType nvarchar(10) ,  --音乐分类
)
select * from MusicListType
insert into MusicListType values
(2,2,'男歌手','爱情'),
(3,3,'男歌手','爱情'),
(4,4,'男歌手','浪漫'),
(5,5,'男歌手','承诺'),
(6,6,'女歌手','结婚'),
(7,7,'女歌手','悲伤'),
(2,1,'男歌手','爱情')

go
--评论表
create table Discuss
(
DiscussID int primary key identity(1,1) not null,  --编号
DiscussContent nvarchar(100) not null,  --评论内容
DiscussTime datetime ,--评论时间
[UserID][int] foreign key([UserID]) references UserMusic([UserID]),  --用户编号
MusicListID [int] foreign key(MusicListID) references MusicList(MusicListID)  --歌曲编号
)
go


insert into Discuss values
('好好听，超级无敌好听！好听到哭泣','2019-5-3',1,5),
('好好听，超级无敌好听！好听到哭泣','2015-5-3',2,1),
('好好听，超级无敌好听！好听到哭泣','2013-5-3',4,5),
('好好听，超级无敌好听！好听到哭泣','2012-5-3',2,6),
('好好听，超级无敌好听！好听到哭泣','2016-5-3',3,7),
('好好听，超级无敌好听！好听到哭泣','2019-5-15',5,8)
select * from Discuss