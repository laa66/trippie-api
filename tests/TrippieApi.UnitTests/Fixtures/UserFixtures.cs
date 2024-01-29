using Entities;

namespace TrippieApi.UnitTests.Fixtures;

public static class UserFixtures
{
    public static User GetUser() => new() {
        UserLogin = "userlogin1",
        JoinDate = new DateTime(),
    };
}