
using System.Web.Mvc;
using KNT_Shop.Models.Cryptography;

namespace KNT_Shop.Models.SessionModel;

public class SessionModel: Controller
{
    private string _username;
    private int _gioiTinh;
    private string _ngayTao;
    private string _email;
    private string _sdt;
    private string _diaChi;
    private bool _rule;
    private string _token;
    public SessionModel(string tenTaiKhoan) {}

    public SessionModel(string username, int gioiTinh, string ngayTao, string email, string sdt, string diaChi, bool rule)
    {
        _username = username;
        _gioiTinh = gioiTinh;
        _ngayTao = ngayTao;
        _email = email;
        _sdt = sdt;
        _diaChi = diaChi;
        _rule = rule;
        MD5Encrypt md5 = new MD5Encrypt(_username);
        _token = md5.MaHoa;
        
        Session["username"] = _username;
        Session["gioiTinh"] = _gioiTinh;
        Session["ngayTao"] = _ngayTao;
        Session["email"] = _email;
        Session["sdt"] = _sdt;
        Session["diaChi"] = _diaChi;
        Session["rule"] = _rule;
        Session["token"] = _token;
    }

    public string Username
    {
        get => _username;
        set => _username = value;
    }

    public int GioiTinh
    {
        get => _gioiTinh;
        set => _gioiTinh = value;
    }

    public string NgayTao
    {
        get => _ngayTao;
        set => _ngayTao = value;
    }

    public string Email
    {
        get => _email;
        set => _email = value;
    }

    public string Sdt
    {
        get => _sdt;
        set => _sdt = value;
    }

    public string DiaChi
    {
        get => _diaChi;
        set => _diaChi = value;
    }

    public bool Rule
    {
        get => _rule;
        set => _rule = value;
    }

    public string Token
    {
        get => _token;
        set => _token = value;
    }
}