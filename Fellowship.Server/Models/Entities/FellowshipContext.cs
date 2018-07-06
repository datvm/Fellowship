using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Fellowship.Server.Models.Entities
{
    public partial class FellowshipContext : DbContext
    {
        public FellowshipContext()
        {
        }

        public FellowshipContext(DbContextOptions<FellowshipContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountSpecialClaim> AccountSpecialClaim { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupActivity> GroupActivity { get; set; }
        public virtual DbSet<GroupManager> GroupManager { get; set; }
        public virtual DbSet<GroupMember> GroupMember { get; set; }
        public virtual DbSet<GroupMemberActivity> GroupMemberActivity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Fellowship;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.CreatedByAccountId);

                entity.HasIndex(e => e.FacebookId);

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => e.PhoneNumber);

                entity.Property(e => e.FacebookId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountSpecialClaim>(entity =>
            {
                entity.HasIndex(e => e.AccountId);

                entity.HasIndex(e => e.ClaimType);

                entity.Property(e => e.ClaimType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ClaimValue).HasMaxLength(100);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountSpecialClaim)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountSpecialClaim_Account");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasIndex(e => new { e.AdminAccountId, e.Deleted, e.Name })
                    .HasName("IX_Group_Account");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.AdminAccount)
                    .WithMany(p => p.Group)
                    .HasForeignKey(d => d.AdminAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Group_Account");
            });

            modelBuilder.Entity<GroupActivity>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupActivity)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupActivity_Group");
            });

            modelBuilder.Entity<GroupManager>(entity =>
            {
                entity.HasIndex(e => new { e.GroupId, e.Deleted })
                    .HasName("IX_GroupManager_ByGroup");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupManager)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupManager_Group");

                entity.HasOne(d => d.GroupMember)
                    .WithMany(p => p.GroupManager)
                    .HasForeignKey(d => d.GroupMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupManager_GroupMember");

                entity.HasOne(d => d.SetByAccount)
                    .WithMany(p => p.GroupManager)
                    .HasForeignKey(d => d.SetByAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupManager_Account");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasIndex(e => new { e.AccountId, e.Kicked, e.AddedOn })
                    .HasName("IX_GroupMember_MemberOfGroups");

                entity.HasIndex(e => new { e.GroupId, e.Kicked, e.AddedOn })
                    .HasName("IX_GroupMember_GroupMembers");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.FundCache).HasColumnType("money");

                entity.Property(e => e.Nickname).HasMaxLength(100);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.GroupMember)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMember_Account");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMember)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMember_Group");
            });

            modelBuilder.Entity<GroupMemberActivity>(entity =>
            {
                entity.HasIndex(e => new { e.GroupMemberId, e.CreatedTime })
                    .HasName("IX_GroupMemberActivity_MemberActivities");

                entity.HasIndex(e => new { e.LinkedGroupActivityId, e.Fund })
                    .HasName("IX_GroupMemberActivity_LinkedGroupActivityId");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Fund).HasColumnType("money");

                entity.HasOne(d => d.GroupMember)
                    .WithMany(p => p.GroupMemberActivity)
                    .HasForeignKey(d => d.GroupMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMemberActivity_GroupMember");

                entity.HasOne(d => d.LinkedGroupActivity)
                    .WithMany(p => p.GroupMemberActivity)
                    .HasForeignKey(d => d.LinkedGroupActivityId)
                    .HasConstraintName("FK_GroupMemberActivity_GroupActivity");
            });
        }
    }
}
