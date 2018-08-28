﻿namespace GraduationTracker.Repositories
{
    public interface IRepository
    {
        Student GetStudent(int id);
        Diploma GetDiploma(int id);
        Requirement GetRequirement(int id);
    }
}
