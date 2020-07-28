using RapidCMS.Core.Abstractions.Config;
using RapidCMS.Core.Enums;
using RapidCMS.Core.Repositories;
using RapidCMS.Example.Shared.Data;

namespace RapidCMS.Example.Shared.Collections
{
    public static class EntityVariantCollection
    {
        // CRUD editor with support for
        public static void AddEntityVariantCollection(this ICmsConfig config)
        {
            config.AddCollection<EntityVariantBase, BaseRepository<EntityVariantBase>>("variants", "Entity Variants", collection =>
            {
                collection
                    // Set showEntities to true to have this collection to fold open on default
                    .SetTreeView(x => x.Name, showEntitiesOnStartup: true)
                    .AddEntityVariant<EntityVariantA>("Variant A", "a")
                    .AddEntityVariant<EntityVariantB>("Variant B", "b")
                    .AddEntityVariant<EntityVariantC>("Variant C", "c")
                    .SetListEditor(view =>
                    {
                        view.AddDefaultButton(DefaultButtonType.New);
                        view.AddDefaultButton(DefaultButtonType.Return);

                        view.SetColumnVisibility(EmptyVariantColumnVisibility.Visible);

                        view.SetPageSize(10);

                        view
                            .AddSection(section =>
                            {
                                section.AddField(p => p.Id.ToString());

                                section.AddField(p => p.Name)
                                    .SetOrderByExpression(p => p.Name, OrderByType.Ascending);
                            })
                            .AddSection<EntityVariantA>(section =>
                            {
                                section.AddField(p => p.Id.ToString());

                                section.AddField(p => p.Name)
                                    .SetOrderByExpression(p => p.Name, OrderByType.Ascending);

                                section.AddField(p => p.NameA1);

                                section.AddDefaultButton(DefaultButtonType.SaveExisting);
                                section.AddDefaultButton(DefaultButtonType.SaveNew);
                            });

                        view
                            .AddSection<EntityVariantB>(section =>
                            {
                                section.AddField(p => p.Id.ToString());

                                section.AddField(p => p.Name)
                                    .SetOrderByExpression(p => p.Name, OrderByType.Ascending);

                                section.AddField(p => p.NameB1);
                                section.AddField(p => p.NameB2);

                                section.AddDefaultButton(DefaultButtonType.SaveExisting);
                                section.AddDefaultButton(DefaultButtonType.SaveNew);
                            });

                        view
                            .AddSection<EntityVariantC>(section =>
                            {
                                section.AddField(p => p.Id.ToString());

                                section.AddField(p => p.Name)
                                    .SetOrderByExpression(p => p.Name, OrderByType.Ascending);

                                section.AddField(p => p.NameC1);
                                section.AddField(p => p.NameC2);
                                section.AddField(p => p.NameC3);

                                section.AddDefaultButton(DefaultButtonType.SaveExisting);
                                section.AddDefaultButton(DefaultButtonType.SaveNew);
                            });
                    });
            });
        }
    }
}
