Online Exam System | ASP.NET MVC + SQL + Identity

🇹🇷 Proje Hakkında
ASP.NET MVC, SQL ve Identity kullanılarak geliştirilmiş tam özellikli bir çevrim içi sınav sistemidir.
Sistem; kullanıcı kayıt olma, giriş yapma, e-posta (SMTP) üzerinden şifre sıfırlama, rol tabanlı yetkilendirme (öğretmen / öğrenci), test oluşturma, sınav çözme ve sonuç görüntüleme gibi özellikleri içerir.

Kimlik doğrulama ve yetkilendirme ASP.NET Identity altyapısı ile yapılmıştır.
“Şifremi Unuttum” özelliği ile kullanıcı, e-posta adresine gelen güvenli link üzerinden şifresini sıfırlayabilir.
Token üretimi ve doğrulama süreçleri güvenli biçimde uygulanmıştır.

Rol yönetimi sayesinde öğretmen (admin) ve öğrenci rolleri ayrı yetkilere sahiptir.
Öğretmen; test oluşturabilir, soruları düzenleyebilir, sınava katılan öğrencileri ve aldıkları puanları görüntüleyebilir.
Öğrenci; giriş yaptıktan sonra test çözebilir, sınavı sadece bir kez tamamlayabilir ve geçmiş sınav sonuçlarını görebilir.

Veritabanı tarafında kullanıcı, test, soru ve sonuç tabloları birbirleriyle ilişkilendirilmiştir.
Tüm işlemler Entity Framework üzerinden yürütülmektedir.

Kullanılan Teknolojiler:
	•	ASP.NET MVC 5
	•	C#
	•	Entity Framework
	•	MS SQL Server
	•	ASP.NET Identity (UserManager, RoleManager)
	•	HTML, CSS, Bootstrap
	•	SMTP (MailKit veya System.Net.Mail)

Güvenlik ve Özellikler:
	•	Mail üzerinden şifre sıfırlama
	•	Token süresi kontrolü ve doğrulama
	•	HTTPS üzerinden güvenli bağlantı
	•	Rol tabanlı yetkilendirme
	•	Login olmadan test çözme engeli
	•	Admin panelinden kullanıcı rolleri yönetimi

Gelecek Geliştirmeler:
	•	RESTful Web API entegrasyonu (mobil uygulamalar için)
	•	Sınav sonuçlarını grafiksel olarak gösterme (Chart.js)
	•	Modern UI geliştirmesi (Bootstrap 5 / TailwindCSS)
	•	Öğrenci bazlı sınav analizi

Geliştirici:
Mehmet Ali
Software Engineering Student
Backend: ASP.NET MVC, SQL
Mobile: Flutter
GitHub: https://github.com/MehmetAli-03

⸻

🇬🇧 About the Project
A fully functional online exam system built with ASP.NET MVC, SQL, and Identity framework.
It includes user registration, login, password reset via email (SMTP), and role-based authorization (teacher/student).

ASP.NET Identity is used for secure authentication and authorization.
The “Forgot Password” feature allows users to reset their password via a secure email link.
Password reset tokens are safely generated and verified.

Role management separates teacher (admin) and student permissions.
Teachers can create and manage tests, add or edit questions, view which students took the test, and see their scores.
Students can log in, take tests only once, and view their previous results.

The database includes related tables for users, tests, questions, and results, managed through Entity Framework.

Technologies Used:
	•	ASP.NET MVC 5
	•	C#
	•	Entity Framework
	•	MS SQL Server
	•	ASP.NET Identity (UserManager, RoleManager)
	•	HTML, CSS, Bootstrap
	•	SMTP (MailKit or System.Net.Mail)

Security & Features:
	•	Password reset via email
	•	Token generation and verification
	•	Secure HTTPS connection
	•	Role-based authorization
	•	Restriction for non-logged users
	•	Admin panel for managing roles

Future Improvements:
	•	RESTful Web API integration (for mobile support)
	•	Graphical exam results using Chart.js
	•	Modernized UI (Bootstrap 5 / TailwindCSS)
	•	Student-based performance analytics

Developer:
Mehmet Ali
Software Engineering Student
Backend: ASP.NET MVC, SQL
Mobile: Flutter
GitHub: https://github.com/MehmetAli-03
