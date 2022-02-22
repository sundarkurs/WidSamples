using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontifyApiConsumer.Common
{
    public static class Constants
    {
        public static class Queries
        {
            public const string ProjectAssets = @"
                query ProjectAssets {
                    project: node(id: ""eyJpZGVudGlmaWVyIjoxNDIsInR5cGUiOiJwcm9qZWN0In0="") {
                    type: __typename
                    id
                    ...on MediaLibrary {
                                name
                        assets(limit: 5) {
                                    items {
                                        id
                                        title
                                        description
                                        tags {
                                            source
                                            value
                                        }
                                        type: __typename
                                        createdAt

                                        ... on Image {
                                            previewUrl
                            }
                                        ... on Video {
                                            previewUrl
                                        }
                                        ... on Document {
                                            previewUrl
                                            pageCount
                                        }
                                        ... on Audio {
                                            previewUrl
                                        }
                                        ... on File {
                                            previewUrl
                                        }
                                    }
                                }
                            }
                        }
                    }

             ";
        }

        public static class Mutations
        {
            public const string CreateAssetMutation = @"mutation CreateAsset($input: CreateAssetInput!) {
                                                    createAsset(input: $input) { job { assetId } } }";

            public const string DeleteAssetMutation = @"mutation DeleteAsset($input: DeleteAssetInput!) {
                                                    deleteAsset(input: $input) { asset { id } }}";
        }
    }
}
