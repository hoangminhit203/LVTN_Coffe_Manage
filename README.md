# LVTN_Coffe_Manage
Back_End C#
Common web application architectures (Về cấu trúc ) 
Ứng dụng tất cả trong một
Số lượng dự án tối thiểu có thể có cho một kiến trúc ứng dụng là một. Trong kiến trúc này, toàn bộ logic của ứng dụng được chứa trong một dự án duy nhất, được biên dịch thành một assembly duy nhất và được triển khai như một đơn vị duy nhất.
<h2>Ứng dụng tất cả trong một</h2>
Một dự án ASP.NET Core mới, dù được tạo trong Visual Studio hay từ dòng lệnh, đều bắt đầu như một khối đơn giản "tất cả trong một". Nó chứa tất cả các hành vi của ứng dụng, bao gồm logic trình bày, nghiệp vụ và truy cập dữ liệu. Hình 5-1 minh họa cấu trúc tệp của một ứng dụng dự án đơn.
<img width="897" height="502" alt="image" src="https://github.com/user-attachments/assets/8f980aad-4139-4a8f-936a-df994aa62e9c" />
Hình 5-1. Một ứng dụng ASP.NET Core của một dự án duy nhất.

Trong một kịch bản dự án đơn lẻ, việc phân tách các mối quan tâm được thực hiện thông qua việc sử dụng các thư mục. Mẫu mặc định bao gồm các thư mục riêng biệt cho các trách nhiệm của mô hình MVC đối với Models, Views và Controllers, cũng như các thư mục bổ sung cho Data và Services. Trong cách sắp xếp này, chi tiết trình bày nên được giới hạn càng nhiều càng tốt trong thư mục Views, và chi tiết triển khai truy cập dữ liệu nên được giới hạn trong các lớp được lưu trữ trong thư mục Data. Logic nghiệp vụ nên nằm trong các dịch vụ và lớp trong thư mục Models.

Mặc dù đơn giản, giải pháp monolithic cho một dự án đơn lẻ vẫn có một số nhược điểm. Khi quy mô và độ phức tạp của dự án tăng lên, số lượng tệp và thư mục cũng sẽ tiếp tục tăng. Các vấn đề về giao diện người dùng (UI) (mô hình, chế độ xem, bộ điều khiển) nằm trong nhiều thư mục, không được nhóm theo thứ tự bảng chữ cái. Vấn đề này càng trở nên tồi tệ hơn khi các cấu trúc cấp UI bổ sung, chẳng hạn như Bộ lọc hoặc ModelBinder, được thêm vào các thư mục riêng của chúng. Logic nghiệp vụ nằm rải rác giữa các thư mục Mô hình và Dịch vụ, và không có chỉ dẫn rõ ràng về việc lớp nào trong thư mục nào nên phụ thuộc vào lớp nào. Sự thiếu tổ chức ở cấp độ dự án này thường dẫn đến mã spaghetti .

Để giải quyết những vấn đề này, các ứng dụng thường phát triển thành các giải pháp đa dự án, trong đó mỗi dự án được coi là nằm ở một lớp cụ thể của ứng dụng.
<h2>Ứng dụng kiến trúc "N-Layer" truyền thống</h2>
<img width="890" height="492" alt="image" src="https://github.com/user-attachments/assets/b1d8b47c-2a31-4c2c-8fe6-8251e3dc9f66" />
Hình 5-2. Các lớp ứng dụng điển hình.

Các lớp này thường được viết tắt là UI, BLL (Lớp Logic Nghiệp vụ) và DAL (Lớp Truy cập Dữ liệu). Sử dụng kiến trúc này, người dùng thực hiện các yêu cầu thông qua lớp UI, lớp này chỉ tương tác với BLL. BLL, ngược lại, có thể gọi DAL để yêu cầu truy cập dữ liệu. Lớp UI không nên thực hiện bất kỳ yêu cầu nào trực tiếp đến DAL, cũng không nên tương tác trực tiếp với tính bền vững thông qua các phương tiện khác. Tương tự, BLL chỉ nên tương tác với tính bền vững bằng cách thông qua DAL. Theo cách này, mỗi lớp đều có trách nhiệm riêng.

Một nhược điểm của phương pháp phân lớp truyền thống này là các phụ thuộc thời gian biên dịch chạy từ trên xuống dưới. Nghĩa là, lớp UI phụ thuộc vào BLL, và BLL lại phụ thuộc vào DAL. Điều này có nghĩa là BLL, thường nắm giữ logic quan trọng nhất trong ứng dụng, lại phụ thuộc vào chi tiết triển khai truy cập dữ liệu (và thường phụ thuộc vào sự tồn tại của cơ sở dữ liệu). Việc kiểm thử logic nghiệp vụ trong kiến trúc như vậy thường khó khăn, đòi hỏi phải có cơ sở dữ liệu kiểm thử. Nguyên lý đảo ngược phụ thuộc có thể được sử dụng để giải quyết vấn đề này, như bạn sẽ thấy trong phần tiếp theo.
<h3>Doc Để đọc thêm https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures</h3>
<h1>Hướng Dẫn Cấu Trúc Hệ Thống </h1>
<H2> Coffe_Manage.Domain </H2>
<p>📌 Vai trò: Chứa các định nghĩa cốt lõi (Core business logic) và các kiểu dữ liệu dùng chung.
Không phụ thuộc vào tầng nào khác.

Configurations → Nơi định nghĩa các cấu hình chung cho Entity (ví dụ: Fluent API mapping cho EF Core).

Constants → Chứa các hằng số (constant values) như thông báo lỗi, trạng thái, quyền,…

CustomValidationAttribute → Chứa các Attribute để validate dữ liệu (ví dụ [EmailValid], [PhoneNumberValid]).

Enums → Chứa các enum (trạng thái đơn hàng, vai trò user, loại sản phẩm…).

IService → Định nghĩa các interface cho service layer (ví dụ IUserService, IOrderService).

VModels → View Models hoặc DTOs để truyền dữ liệu giữa backend và frontend (khác Entity).</p>
<h2>Coffe_Manage.Infrastructure.EF</h2>
<p>📌 Vai trò: Kết nối với cơ sở dữ liệu bằng Entity Framework.

Context → Chứa DbContext để quản lý kết nối DB và DbSet cho các bảng.

Entities → Chứa các class mô tả bảng trong DB (ví dụ User, Product, Order).

Migrations → Chứa file migration khi chạy Add-Migration và Update-Database.

UnitOfWork.cs → Triển khai Unit of Work pattern để quản lý giao dịch (transaction) giữa nhiều repository.</p>
<h2> Coffe_Manage.Repository </h2>
<p>📌 Vai trò: Tầng làm việc trực tiếp với database thông qua EF Core (hoặc ORM khác).

Thường có các repository class như UserRepository, ProductRepository để thao tác CRUD.

Bạn đang có 2 folder NewFolder và NewFolder1 → Có thể là thư mục placeholder, chưa đặt tên.</p>
<h2>Coffe_Manage.Service </h2>
<p>📌 Vai trò: Chứa business logic của ứng dụng.

Helpers → Các hàm hỗ trợ chung (ví dụ: convert data, format string, gửi email…).

MiddleWare → Chứa middleware custom (ví dụ: logging, handle exception, authentication…).</p>
