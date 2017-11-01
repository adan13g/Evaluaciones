namespace Mysql_Asp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;

    public partial class ModeloDatos : DbContext
    {
        public ModeloDatos()
            : base("DefaultConnection")
        {
        }
        public Guid Random()
        {
            return new Guid();
        }
        public virtual DbSet<tbl_alumnos> tbl_alumnos { get; set; }
        public virtual DbSet<tbl_asignacion> tbl_asignacion { get; set; }
        public virtual DbSet<tbl_calificacion> tbl_calificacion { get; set; }
        public virtual DbSet<tbl_carreras> tbl_carreras { get; set; }
        public virtual DbSet<tbl_estimacion> tbl_estimacion { get; set; }
        public virtual DbSet<tbl_grupos> tbl_grupos { get; set; }
        public virtual DbSet<tbl_materias> tbl_materias { get; set; }
        public virtual DbSet<tbl_preguntas> tbl_preguntas { get; set; }
        public virtual DbSet<tbl_profesores> tbl_profesores { get; set; }
        public virtual DbSet<tbl_respuestas> tbl_respuestas { get; set; }
        public virtual DbSet<tbl_semestres> tbl_semestres { get; set; }
        public virtual DbSet<tbl_status> tbl_status { get; set; }
        public virtual DbSet<tbl_subunidad> tbl_subunidad { get; set; }
        public virtual DbSet<tbl_unidad> tbl_unidad { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_alumnos>()
                .Property(e => e.matricula)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_alumnos>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_alumnos>()
                .Property(e => e.ape_pat)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_alumnos>()
                .Property(e => e.ape_mat)
                .IsUnicode(false);

           

            modelBuilder.Entity<tbl_alumnos>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_alumnos>()
                .Property(e => e.estate)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_alumnos>()
                .HasMany(e => e.tbl_calificacion)
                .WithRequired(e => e.tbl_alumnos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_asignacion>()
                .Property(e => e.siglema)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_asignacion>()
                .Property(e => e.id_profesor)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_asignacion>()
                .HasMany(e => e.tbl_calificacion)
                .WithRequired(e => e.tbl_asignacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_calificacion>()
                .Property(e => e.matricula)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_carreras>()
                .Property(e => e.carrera)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_carreras>()
                .HasMany(e => e.tbl_asignacion)
                .WithRequired(e => e.tbl_carreras)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_estimacion>()
                .Property(e => e.id_profesor)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_grupos>()
                .Property(e => e.grupos)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_grupos>()
                .HasMany(e => e.tbl_alumnos)
                .WithRequired(e => e.tbl_grupos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_grupos>()
                .HasMany(e => e.tbl_asignacion)
                .WithRequired(e => e.tbl_grupos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_grupos>()
                .HasMany(e => e.tbl_estimacion)
                .WithRequired(e => e.tbl_grupos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_materias>()
                .Property(e => e.siglema)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_materias>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_materias>()
                .HasMany(e => e.tbl_asignacion)
                .WithRequired(e => e.tbl_materias)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_materias>()
                .HasMany(e => e.tbl_unidad)
                .WithRequired(e => e.tbl_materias)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_preguntas>()
                .Property(e => e.pregunta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_preguntas>()
                .Property(e => e.id_profesor)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_preguntas>()
                .HasMany(e => e.tbl_respuestas)
                .WithRequired(e => e.tbl_preguntas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_profesores>()
                .Property(e => e.id_profesor)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_profesores>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_profesores>()
                .Property(e => e.ape_pat)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_profesores>()
                .Property(e => e.ape_mat)
                .IsUnicode(false);

      
            modelBuilder.Entity<tbl_profesores>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_profesores>()
                .Property(e => e.estate)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_profesores>()
                .HasMany(e => e.tbl_asignacion)
                .WithRequired(e => e.tbl_profesores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_profesores>()
                .HasMany(e => e.tbl_estimacion)
                .WithRequired(e => e.tbl_profesores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_profesores>()
                .HasMany(e => e.tbl_preguntas)
                .WithRequired(e => e.tbl_profesores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_respuestas>()
                .Property(e => e.respuesta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_semestres>()
                .Property(e => e.semestre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_semestres>()
                .HasMany(e => e.tbl_materias)
                .WithRequired(e => e.tbl_semestres)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_status>()
                .Property(e => e.estatus)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_status>()
                .HasMany(e => e.tbl_alumnos)
                .WithRequired(e => e.tbl_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_status>()
                .HasMany(e => e.tbl_grupos)
                .WithRequired(e => e.tbl_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_status>()
                .HasMany(e => e.tbl_materias)
                .WithRequired(e => e.tbl_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_status>()
                .HasMany(e => e.tbl_preguntas)
                .WithRequired(e => e.tbl_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_status>()
                .HasMany(e => e.tbl_profesores)
                .WithRequired(e => e.tbl_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_status>()
                .HasMany(e => e.tbl_semestres)
                .WithRequired(e => e.tbl_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_status>()
                .HasMany(e => e.tbl_subunidad)
                .WithRequired(e => e.tbl_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_status>()
                .HasMany(e => e.tbl_unidad)
                .WithRequired(e => e.tbl_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_subunidad>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_subunidad>()
                .HasMany(e => e.tbl_calificacion)
                .WithRequired(e => e.tbl_subunidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_subunidad>()
                .HasMany(e => e.tbl_estimacion)
                .WithRequired(e => e.tbl_subunidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_subunidad>()
                .HasMany(e => e.tbl_preguntas)
                .WithRequired(e => e.tbl_subunidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_unidad>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_unidad>()
                .Property(e => e.siglema)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_unidad>()
                .HasMany(e => e.tbl_subunidad)
                .WithRequired(e => e.tbl_unidad)
                .WillCascadeOnDelete(false);


        }
    }
}
