namespace IdSecure
{
    public class IdSecureComp
    {
        public int GetIdSecure(int idUser)
        {
            return new MySqlDataAccess().GetIdUserSecure(idUser);
        }

        public int GetIdUserValid(int idSecure)
        {
            return new MySqlDataAccess().GetIdUserValid(idSecure);
        }
    }
}
