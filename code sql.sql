Create database PetKingdom
create table blog_category(
	id nvarchar(50)  constraint pk_blog_category primary key,
	name nvarchar(100) unique not null,
	description nvarchar(500) null,
	status int not null
)



create table blog(
	id nvarchar(50)  constraint pk_blog primary key,
	name nvarchar(100) unique not null,
	author nvarchar(100) not null,
	content nvarchar(max) not null,
	thumbnail nvarchar(300) not null,
	Created_date date,
	status int not null,
	blog_category_id nvarchar(50) foreign key(blog_category_id) references blog_category(id)
)




create table product_category(
	id nvarchar(50)  constraint pk_product_category primary key,
	name nvarchar(100) unique not null,
	description nvarchar(500) null,
	status int not null
)
Create table group_product(
	id nvarchar(50)  constraint pk_group_product primary key,
	name nvarchar(100) unique not null,
	description nvarchar(500) null,
	status int not null
)
Create table brand(
	id nvarchar(50)  constraint pk_brand primary key,
	name nvarchar(100) unique not null,
	thumbnail nvarchar(300) not null,
	description nvarchar(500) null,
	status int not null
)
Create table banner(
id nvarchar(50) constraint pk_banner primary key,
thumbnail nvarchar(200) not null,
link nvarchar(300) not null,
created_date date,
status int,
active_as_slide bit,
product_category_id nvarchar(50) foreign key(product_category_id) references product_category(id)
)
Create table product(
	id nvarchar(50) constraint pk_product primary key,
	name nvarchar(200)  not null,
	weight int not null,
	brief_description nvarchar(max),
	full__description nvarchar(max) null,
	total_quatity int not null,
	lowest_inventory_level int,
	highest_inventory_level int,
	Created_date date,
	Update_date date,
	status int not null,
	product_category_id nvarchar(50) foreign key(product_category_id) references product_category(id),
	group_product_id nvarchar(50) foreign key(group_product_id) references group_product(id),
	brand_id nvarchar(50) foreign key(brand_id) references brand(id),

)

Create table rating(
	id nvarchar(50) constraint pk_rating primary key,
	score int not null,
	poster_name nvarchar(100),
	poster_email nvarchar(100),
	poster_phonenumber nvarchar(30),
	content nvarchar(300),
	made_purchase bit,
	recommend_to_relatives bit,
	product_id nvarchar(50) foreign key(product_id) references product(id),
	status int not null,

)

Create table comment(
	id nvarchar(50) constraint pk_comment primary key,
	poster_name nvarchar(100),
	poster_email nvarchar(100),
	poster_phonenumber nvarchar(30),
	content varchar(300),
	product_id nvarchar(50) foreign key(product_id) references product(id),
	parent_comment_id nvarchar(50) null,
	status int not null,

)

Create table product_image(
	id nvarchar(50) constraint pk_product_image primary key,
	link nvarchar(200),
	status int,
	product_id nvarchar(50) foreign key(product_id) references product(id),

)

Create table product_sell_price(
id nvarchar(50) constraint pk_sell_price primary key,
unit_price int,
status int,
product_id nvarchar(50) foreign key(product_id) references product(id),
)
Create table product_discount(
id nvarchar(50) constraint pk_product_discount primary key,
discount_amount float,
status int,
product_id nvarchar(50) foreign key(product_id) references product(id),
)
Create table inventory(
id nvarchar(50) constraint pk_consignment primary key,
manufacture_date date not null,
expiration_date date not null,
receipt_price int,
Created_date date,
status int,
product_id nvarchar(50) foreign key(product_id) references product(id),
)

Create table account(
id nvarchar(50) constraint pk_account primary key,
permission nvarchar(100) not null,
username nvarchar(100) not null unique,
password nvarchar(100) not null,
security_stamp nvarchar(100) null,
concurrency_stamp nvarchar(100) null,
Access_failed_count int null,
lock_account bit null,
lock_time datetime null,
phone_confirmed bit not null,

)

Create table employee(
id nvarchar(50) constraint pk_employee primary key,
first_name nvarchar(50) not null,
last_name varchar(100) not null,
phonenumber nvarchar(30) not null,
address nvarchar(200) not null,
email nvarchar(200) not null,
birthday date not null,
identity_card_number nvarchar(30) not null,
sex nvarchar(10),
status int,
account_id  nvarchar(50) foreign key(account_id) references account(id),

)

Create table customer(
id nvarchar(50) constraint pk_customer primary key,
first_name nvarchar(50) not null,
last_name varchar(100) not null,
phonenumber nvarchar(30) not null,
address nvarchar(200) not null,
email nvarchar(200) not null,
sex nvarchar(10),
image nvarchar(100) null,
status int,
account_id  nvarchar(50) foreign key(account_id) references account(id),)

Create table pet(
id nvarchar(50) constraint pk_pet primary key,
name nvarchar(100) not null,
weight int null,
birthday date null,
image nvarchar(100),
specices nvarchar(100) null,
 breed nvarchar(100) null,
 sex nvarchar(100) not null,
 health_note nvarchar(max) null,
 status int,
 customer_id  nvarchar(50) foreign key(customer_id) references customer(id),)

Create table provider(
id nvarchar(50) constraint pk_provider primary key,
name nvarchar(200) not null,
address nvarchar(500) not null,
phonenumber nvarchar(30) not null,
tax_number nvarchar(30) not null,
email nvarchar(100) not null,
status int
)
Create table receipt_bill(
id nvarchar(50) constraint pk_receipt_bill primary key,
Created_date date not null,
Total_paid bigint not null,
 provider_id  nvarchar(50) foreign key(provider_id) references provider(id),
 employee_account_id  nvarchar(50) foreign key(employee_account_id) references account(id),
 status int )

 Create table receipt_bill_details(
 id nvarchar(50) constraint pk_receipt_bill_details primary key,
 product_name nvarchar(200) not null,
 unit_price int not null,
 quantity int not null,
 discount float not null,
 grand_paid bigint not null,
 status int,
 product_id nvarchar(50) foreign key(product_id) references product(id),
 receipt_bill_id  nvarchar(50) foreign key(receipt_bill_id) references receipt_bill(id),)

 Create table sell_bill(
 id nvarchar(50) constraint pk_sell_bill primary key,
Created_date date not null,
Total_paid bigint not null,
 customer_account_id  nvarchar(50) foreign key(customer_account_id) references account(id),
 employee_account_id  nvarchar(50) foreign key(employee_account_id) references account(id),
 status int 
 )


  Create table sell_bill_details(
 id nvarchar(50) constraint pk_sell_bill_details primary key,
 product_name nvarchar(200) not null,
 receipt_price int not null,
 unit_price int not null,
 quantity int not null,
 discount float not null,
 grand_paid bigint not null,
 status int,
 product_id nvarchar(50) foreign key(product_id) references product(id),
sell_bill_id  nvarchar(50) foreign key(sell_bill_id) references sell_bill(id),)

 Create table pet_service(
	id nvarchar(50) constraint pk_pet_service primary key,
	name nvarchar(200) not null,
	full_desciption nvarchar(max) not null,
	brief_description nvarchar(max) not null,
	thumbnail nvarchar(300) not null,
	icon nvarchar(100) not null,
	status int
 )

 Create table service_option(
 	id nvarchar(50) constraint pk_service_option primary key,
	 pet_service_id  nvarchar(50) foreign key(pet_service_id) references pet_service(id),
	 name nvarchar(200) not null,
	 description nvarchar(max) ,
	 estimated_completion_time int,
	 status int
 )


Create table service_sell_price(
id nvarchar(50) constraint pk_service_sell_price primary key,
unit_price int,
pet_minimum_weight float,
pet_maximum_weight float,
status int,
service_option_id  nvarchar(50) foreign key(service_option_id) references service_option(id),
)

Create table schedule_available(
id nvarchar(50) constraint pk_booking_available primary key,
started_date date not null,
ended_date date not null,
available_hour time not null,
status int,
service_option_id  nvarchar(50) foreign key(service_option_id) references service_option(id),

)

Create table schedule(
id nvarchar(50) constraint pk_schedule primary key,
service_option_name nvarchar(200) not null,
booking_date date not null,
booking_hour time not null,
grand_paid int,
status int,
service_option_id  nvarchar(50) foreign key(service_option_id) references service_option(id),
sell_bill_id  nvarchar(50) foreign key(sell_bill_id) references sell_bill(id),
pet_id nvarchar(50) foreign key (pet_id) references pet(id)
) 

Create table shift(
id nvarchar(50) constraint pk_shift primary key,
working_date date not null,
started_hour time not null,
ended_hour time not null,
schedule_id nvarchar(50) foreign key (schedule_id) references schedule(id),
caring_staff_id nvarchar(50) foreign key (caring_staff_id) references account(id),
status int
)

Create table process_shift(
id nvarchar(50) constraint pk_process_shift primary key,
link nvarchar(200) not null,
created_date date not null,
status int,
shift_id  nvarchar(50) foreign key(shift_id) references shift(id),

)
Create table service_image(
	id nvarchar(50) constraint pk_service_image primary key,
	link nvarchar(200),
	status int,
	service_id nvarchar(50) foreign key(service_id) references Pet_service(id),

)

Insert into pet_service(id, name, full_desciption, brief_description, thumbnail, icon, status)
values(NEWID(), N'Tắm - vệ sinh thú cưng', N'Dịch vụ tắm thú cưng', N'Dịch vụ tắm thú cưng','/default.png', '/default.png', 0),
(NEWID(), N'Cắt tỉa lông thú cưng', N'Cắt tỉa lông thú cưng', N'Cắt tỉa lông thú cưng','/default.png', '/default.png', 0),
(NEWID(), N'Dắt chó đi dạo', N'Dắt chó đi dạo', N'Dắt chó đi dạo','/default.png', '/default.png', 0)

Insert into account(id,permission,username,password,phone_confirmed)
values(NEWID(), N'Admin', 'anhtukc', '01663616371HY', 0),
(NEWID(), N'CaringStaff', 'mailinh98', '12345678', 0),
(NEWID(), N'ManagementStaff', 'thuylinh', '12345678', 0)